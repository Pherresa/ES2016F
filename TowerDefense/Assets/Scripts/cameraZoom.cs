using UnityEngine;
using System.Collections;

public class cameraZoom : MonoBehaviour {

	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis

	public float zoomSpeed = 4.0f;		// Speed of the camera whn being zoomed in or out

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts

	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?

	//
	// UPDATE
	//

	void Update () 
	{
		// Get the left mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
			

		// Get the right mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating=false;
		if (!Input.GetMouseButton(2)) isZooming=false;
	

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed); 
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}


		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			Debug.Log ("Is zooming");

			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);
			Debug.Log ("Is zooming");
		}
	}
}


