using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	// Start is called before the first frame update
	float speed = 5f;
	public bool open = false;
	Transform leftDoor, rightDoor;
	Vector3 startPosLeft, startPosRight, openPosLeft, openPosRight;
	public GameObject respawnPoint;
    void Start()
    {
		leftDoor = transform.GetChild(1).transform;
		rightDoor = transform.GetChild(2).transform;

		startPosLeft = leftDoor.localPosition;
		startPosRight = rightDoor.localPosition;

		openPosLeft = new Vector3(leftDoor.localPosition.x, leftDoor.localPosition.y, leftDoor.localPosition.z - 2);
		openPosRight = new Vector3(rightDoor.localPosition.x, rightDoor.localPosition.y, rightDoor.localPosition.z + 2);

	}

    // Update is called once per frame
    void Update()
    {
		if (open)
		{
			leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, openPosLeft, Time.deltaTime * speed);
			rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, openPosRight, Time.deltaTime * speed);
			//leftDoor.localPosition = openPosLeft;
			//rightDoor.localPosition = openPosRight;
		}
		else
		{
			leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, startPosLeft, Time.deltaTime * speed);
			rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, startPosRight, Time.deltaTime * speed);
		}
    }
}
