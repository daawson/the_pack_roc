using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeoController : MonoBehaviour
{
	// Start is called before the first frame update
	public float maxTimer = 400;
	public float timer = 0;
	public float delay = 0;

	ParticleSystem ps;

	void Start()
    {
		timer -= delay;
		ps = GetComponentInChildren<ParticleSystem>();
		ps.Stop();
    }

	// Update is called once per frame
	void Update()
    {
		timer++;
		if(timer == maxTimer)
		{
			timer = 0;
			if (!ps.isEmitting)
				ps.Play();
			else
				ps.Stop();
		}
    }
}
