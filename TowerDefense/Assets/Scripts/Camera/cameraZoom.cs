using UnityEngine;
using System.Collections;

public class cameraZoom : MonoBehaviour {

	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis

	public float zoomSpeed = 15.0f;		// Speed of the camera whn being zoomed in or out

	public float panSpeed = 20.0f;

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts

	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?
	private bool isPanning;


	public float ZoomAmount = 0;
	public float MaxToClamp = 13; 



	public float speed = 30.0f;

	//
	// UPDATE
	//






	public float minX = -45.0f;
	public float maxX = 45.0f;

	public float minY = -60.0f;
	public float maxY = 45.0f;
 
	public float sensX = 100.0f;
	public float sensY = 100.0f;
	
	float rotationY = 0.0f;
	float rotationX = 0.0f;





	void Update () 
	{


		// Get the right mouse button
		if (Input.GetMouseButton (1)) {

			/*rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY -= Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			rotationX = Mathf.Clamp (rotationX, minX, maxX);
			Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.0f * Time.deltaTime);	

*/

			rotationX = Input.GetAxis("Mouse X");
			rotationY = Input.GetAxis("Mouse Y");
			Camera.main.transform.Rotate(new Vector3(rotationX*speed*Time.deltaTime, rotationY*speed*Time.deltaTime, 0));	
		}












		// Get the wheel mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating=false;
		if (!Input.GetMouseButton(2)) isPanning=false;
	
/*
		ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
		ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);

		var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
		transform.Translate(0,0,translate * zoomSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));*/

		if(Input.GetAxis("Mouse ScrollWheel")!=0){
			Camera.main.transform.Translate(new Vector3(0,0, speed*Time.deltaTime*Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"))));
		}


		if(isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			float y;
			Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
	       	transform.Translate(move, Space.Self);

	       	y = transform.position.y;

	       	transform.position = new Vector3(Mathf.Clamp(transform.position.x, -115.0f, 135.0f), Mathf.Clamp(y, 20.0f,100.0f), Mathf.Clamp(transform.position.z, -190.0f, 50.0f)); 
	       

/*
			rotationX = Input.GetAxis("Mouse X");
			rotationY = Input.GetAxis("Mouse Y");
			//Camera.main.transform.Translate(new Vector3(-pos.x*speed*Time.deltaTime, -pos.y*speed*Time.deltaTime, 0), Space.Self);	
			//Camera.main.transform.Translate(new Vector3(Mathf.Sign(pos.x)*speed*Time.deltaTime, Mathf.Sign(pos.y)*speed*Time.deltaTime, 0), Space.Self);
			Camera.main.transform.Translate(new Vector3(Mathf.Sign(rotationX)*speed*Time.deltaTime, Mathf.Sign(rotationY)*speed*Time.deltaTime, 0));	*/
		}

	}
}



