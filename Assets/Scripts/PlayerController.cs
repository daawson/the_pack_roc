using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	// Getting the Game Controller
	GameController gc;

	// Health
	public int health = 100;
	public int regenTimer = 0;
	public int regenTimerMax = 200;

	// Movement
	Rigidbody rb;
	public float mSpeed = 50f;
	public float sprintMultiplier = 1.25f;
	public float rSpeed = 100f;

	public float jumpForce = 100f;
	float distToGround;

	public float vertical, horizontal;
	public bool isOnTrampoline = false;

	// animator
	public Animator anim;
	// helper cube grabbing
	GameObject grabbedObject;
	FixedJoint grabbedJoint;


	// LEVEL RESPAWN POINT
	public GameObject respawnPoint;

	// Particle System
	ParticleSystem ps;

	public List<AudioClip> audioClips = new List<AudioClip>();


	void Start()
    {

		// Handlers of game controllers.
		gc = GameController.instance;
		gc.playerController = this;

		// components
		rb = GetComponent<Rigidbody>();
		ps = GetComponent<ParticleSystem>();

		// jump prepare
		distToGround = GetComponent<BoxCollider>().bounds.extents.y;

		// animator
		anim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
	}

	private void Update()
	{
		// If health is not 100, start regen timer.
		if(health != 100)
		{
			regenTimer++;
			if(regenTimer > regenTimerMax)
			{
				health++;
			}
		} else { regenTimer = 0; }	// player healed, reset timer.
	}

	void FixedUpdate()
	{

		// Controls on axis -1 - +1 on WSAD
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");

		// On Space pressed
		if (Input.GetAxis("Jump") > 0)
		{
			if (IsGrounded() && !isOnTrampoline)
			{
				rb.AddForce(transform.up * jumpForce);
				GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
			}
			else if (IsGrounded() && isOnTrampoline)
			{
				rb.AddForce(transform.up * (jumpForce * 2.2f));
				GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
			}
		}

		// Movement magic, manipulating velocity so it stays in constant smooth movement, and still controllable in air, modified to work with sprint multiplier.
		Vector3 velocity = (-transform.right * vertical) * (sprintMultiplier * mSpeed) * Time.fixedDeltaTime;
		velocity.y = rb.velocity.y;
		rb.velocity = velocity;
		transform.Rotate((transform.up * horizontal) * rSpeed * Time.fixedDeltaTime);

		// Anim and particles start on moving forward or backward.
		if (vertical != 0)
		{
			ps.Emit(1);
			anim.SetBool("isWalking", true);
		}
		else
		{
			ps.Stop();
			anim.SetBool("isWalking", false);
		}

		anim.SetBool("isJumping", !IsGrounded());

		// Sprint
		if (Input.GetKey(KeyCode.LeftShift))
			sprintMultiplier = 1.25f;
		else sprintMultiplier = 1;

		// Helper Cube Grabbing
		if (Input.GetMouseButton(0))
		{
			if (!grabbedJoint)
			{
				RaycastHit hit;
				int layerMask = 1 << 8;
				layerMask = ~layerMask;
				if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right), out hit, 2.5f, layerMask))
				{
					if (hit.transform.tag == "HelperCube")
					{
						grabbedObject = hit.transform.gameObject;
						grabbedObject.GetComponent<Rigidbody>().mass = 0f;
						grabbedJoint = gameObject.AddComponent<FixedJoint>() as FixedJoint;
						grabbedJoint.breakForce = 0.0005f;
						grabbedJoint.breakTorque = 0.0005f;
						grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y + 0.1f, grabbedObject.transform.position.z);
						grabbedJoint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
						GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
					}
				}
			}
		}
		else
		{
			if (grabbedJoint)
			{
				grabbedObject.GetComponent<Rigidbody>().mass = 1;
				grabbedObject.GetComponent<Rigidbody>().WakeUp();
				Destroy(grabbedJoint);
			}
		}


		// FALL CHECK
		if (transform.position.y < -10)
		{
			transform.position = respawnPoint.transform.position;
			GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
			rb.velocity = Vector3.zero;
		}

	}

	// Grounded check
	public bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround - 0.4927633f + 0.015f);
	}

	// Collisions checks
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == ("Cigarette"))
		{
			Destroy(collision.gameObject);
			gc.cigsFound++;
		}
		
	}

	// Triggers check
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Trampoline")
		{
			isOnTrampoline = true;
		}		

		if(other.gameObject.tag == "Door")
		{
			this.respawnPoint = other.gameObject.GetComponent<DoorController>().respawnPoint;
		}

		if (other.gameObject.tag == "Exit")
		{
			GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
			SceneManager.LoadScene("RestartScene");
		}


	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Trampoline")
		{
			isOnTrampoline = false;
		}

		if(other.gameObject.tag == "Platform")
		{
			transform.parent = null;
		}

		
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Trampoline")
		{
			isOnTrampoline = true;
		}

		if (other.gameObject.tag == "Platform")
		{
			transform.parent = other.transform;
		}

	}
	// Player is hurt, resets regen timer.
	public void PlayerHit()
	{
		regenTimer = 0;
		if(health > 0)
			this.health-=2;
	}
}