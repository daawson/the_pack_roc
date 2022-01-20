using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
	// Start is called before the first frame update
	public DoorController target_object;
	HingeJoint switchObject;
	bool played = false;
    void Start()
    {
		switchObject = GetComponent<HingeJoint>();
    }

	// Update is called once per frame
	void Update()
	{
		if (switchObject.angle >= 85 && !played)
		{
			target_object.open = true;
			GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Player").GetComponent<PlayerController>().audioClips[6]);
			played = true;
		}
	}

}