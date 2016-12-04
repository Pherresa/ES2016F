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
			Debug.Log ("Nothing Selected");
		} else {
			if ( checkSelectable(pressedObject) && pressedObject.GetInstanceID () == overObject.GetInstanceID () ){
				if (selectedObject != null) {
					deselect (selectedObject);
				}
				selectedObject = pressedObject;
				selectedObject.GetComponent<MeshRenderer> ().enabled = true;
				selectedObject.GetComponent<Renderer> ().material.color = Color.green;
                selectedObject.GetComponent<Slot>().unit.GetComponentInChildren<Projector>().enabled = true;

                Debug.Log (selectedObject.name+" Selected");
			} else {
				if (selectedObject != null) {
					deselect (selectedObject);
				}
				selectedObject = null;
				Debug.Log ("Nothing Selected");
			}
		}
	}

	void deselect(GameObject obj){
		obj.GetComponent<MeshRenderer> ().enabled = false;
        obj.GetComponent<Slot>().unit.GetComponentInChildren<Projector>().enabled = false;
    }

	bool checkSelectable(GameObject obj){
		if (pressedObject.name.StartsWith ("Slot")) {
			return pressedObject.GetComponent<Slot> ().isOccupied;
		} else { 
			return false;
		}
	}


}
