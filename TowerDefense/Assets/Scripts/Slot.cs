using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

	public bool isPath;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getIsPath(){
		return isPath;
	}


	public void SetActive(bool active){
		gameObject.SetActive(active);
	}


}
