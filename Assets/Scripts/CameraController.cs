using UnityEngine;
using System.Collections;

/// <summary>
/// Defines angle limits as the maximum deviation away from the rotation of this object.
/// (in other words: if the yawlimit is 45, then you can only move up to 45 degrees away from this rotation in both directions. 
/// This means the total angle available would be an angle of 90 degrees)
/// An angle of 180 allows complete freedom of movement on that axis.
/// </summary>
/// 
public class CameraController : MonoBehaviour
{
	[SerializeField]
	private OrbitCamera _cam;

	private Vector3 _prevMousePos;
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	void Update()
	{
		Vector3 mPos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);		
		Vector3 mouseDelta = mPos*10 - _prevMousePos;			
		Vector3 moveDelta = mouseDelta * (360f / Screen.height);
		_cam.Move(moveDelta.x, -moveDelta.y);		
		_prevMousePos = mPos;
	}
}