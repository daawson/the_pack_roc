using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	// Start is called before the first frame update
	ParticleSystem ps;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
	}

	void OnParticleCollision(GameObject other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerController>().PlayerHit();
		}
	}
}
