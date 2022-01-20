using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	// vars
	public float timer;
	public float maxTimer = 500;

	public float speed = 10;
	public float distance;

	Vector3 startPos, nextPos;

	public bool isReversing = false;
	public enum dir
	{
		horizontal,
		vertical
	};

	public dir Dir = dir.horizontal;
    void Start()
    {
		startPos = transform.position;
		if (Dir == dir.horizontal)
			nextPos = new Vector3(startPos.x, startPos.y, startPos.z + distance);
		else
			nextPos = new Vector3(startPos.x + distance, startPos.y, startPos.z);
	}

    // Update is called once per frame
    void FixedUpdate()
    {

		if (Dir == dir.horizontal)
			nextPos = new Vector3(startPos.x, startPos.y, startPos.z + distance);
		else
			nextPos = new Vector3(startPos.x + distance, startPos.y, startPos.z);

		timer++;
        if(timer == maxTimer)
		{
			timer = 0;
			isReversing = !isReversing;			
		}

		if (isReversing)
			transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.fixedDeltaTime * speed);
		else
			transform.position = Vector3.MoveTowards(transform.position, startPos, Time.fixedDeltaTime * speed);
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "HelperCube")
		{
			other.gameObject.transform.parent = transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{

		if (other.gameObject.tag == "HelperCube")
		{
			other.gameObject.transform.parent = null;
		}
	}
}
