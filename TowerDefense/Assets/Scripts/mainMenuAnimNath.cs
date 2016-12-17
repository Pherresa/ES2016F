using UnityEngine;
using System.Collections;

public class mainMenuAnimNath : MonoBehaviour {
    public GameObject nath;
	// Use this for initialization
	void Start () {
        nath.GetComponent<Animation>().wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
