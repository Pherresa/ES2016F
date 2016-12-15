using UnityEngine;
using System.Collections;

public class ShootingBall : MonoBehaviour {

    GameObject objec;

    int velo;

	// Use this for initialization
	void Start () {
        velo = 100;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.LookAt(objec.transform.position);
        this.transform.Translate(Vector3.forward * velo * Time.deltaTime);
    }

    public void setTarget(GameObject t) {
        objec = t;
    }
}
