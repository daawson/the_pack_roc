using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	// Start is called before the first frame update
	public Vector3 startPos;
    void Start()
    {
		startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
		{
			transform.position = startPos;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
    }
}
