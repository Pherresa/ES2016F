using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	public GameObject selectedObject;
	public GameObject pressedObject;
	public GameObject overObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//ray from the camera to the mouse position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
		RaycastHit hitInfo; //information about what we hit


		if (Physics.Raycast (ray, out hitInfo)) {
			//gameObject the mouse is currently over
			overObject = hitInfo.transform.gameObject; 
			//Debug.Log("Mouse is over: " + overObject.name );
			//TO DO: Do Something to the gameObject when the mouse is over?

		} else {
			overObject = null;
			//Debug.Log ("Mouse NONE");
		}

		if(Input.GetButtonDown("Fire1")){ //Mouse Down
			//Debug.Log("Mouse down: " + overObject.name );
			press ();
		}

		if(Input.GetButtonUp("Fire1")){ //Mouse Up
			//Debug.Log("Mouse up: " + overObject.name );
			select ();
		}
	}

	void press(){
		//TO DO: Do Something when an object is pressed?
		pressedObject = overObject;
	}

	void select(){
		if (overObject == null) {
			selectedObject = null;
		}
		if (pressedObject.GetInstanceID () == overObject.GetInstanceID ()) {
			selectedObject = pressedObject;
		} else {
			selectedObject = null;
		}
	}
}
