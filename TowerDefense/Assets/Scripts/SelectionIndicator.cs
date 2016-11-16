using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {

	MouseManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager> ();
		this.setActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (mm.selectedObject != null) {
			Debug.Log("Selected: "+mm.selectedObject.name);
			/*
			try{

			Renderer r = mm.selectedObject.GetComponent<Renderer> ();

			//This diameter only works well for relatively circular or square objects.
			float x = r.bounds.size.x;
			x = x * 1.3f;

				this.transform.position =  new Vector3 (mm.selectedObject.transform.position.x, mm.selectedObject.transform.position.y, mm.selectedObject.transform.position.z);
			this.transform.localScale = new Vector3 (x, x, 1);

			setActive (true);
			}
			catch (MissingComponentException e){
				Debug.Log ("Renderer not found of the selected Object");
			}*/

		} else {
			Debug.Log("Selected: None");
			setActive (false);
		}
	}

	void setActive(bool en){
		this.gameObject.GetComponent<MeshRenderer> ().enabled = en;
	}
}
