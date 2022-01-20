using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	// Start is called before the first frame update
	public DoorController target_object;
	Transform buttonTop;
	bool isTriggered = false;

    void Start()
    {
		buttonTop = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
		if (isTriggered)
		{
			target_object.open = true;
			buttonTop.localPosition = Vector3.Lerp(buttonTop.localPosition, new Vector3(buttonTop.localPosition.x, -0.08f, buttonTop.localPosition.z), Time.deltaTime * 10f);
		}
		else
		{
			target_object.open = false;
			buttonTop.localPosition = Vector3.Lerp(buttonTop.localPosition, new Vector3(buttonTop.localPosition.x, 0.05024355f, buttonTop.localPosition.z), Time.deltaTime * 10f);
		}
		
    }
	
	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "HelperCube")
		{
			isTriggered = true;
		}
	}
	private void OnTriggerStay(Collider collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "HelperCube")
		{
			isTriggered = true;
		}
	}
	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "HelperCube")
		{
			isTriggered = false;
		}
	}

}