using UnityEngine;
using System.Collections;
using System;

public class Elephant : MonoBehaviour {


	Animation anim;
	AnimationState moving;
	AnimationState attack;
	private Vector3 newPos;
	GameObject target;
	public Vector3 center;
	public int range = 60;

	DateTime timeOnPlay;


	private Vector3 newForward;
	private Transform model;

	// Use this for initialization
	void Start () {
		//model = this.transform.GetChild (0);
		//anim = model.GetComponent<Animation> ();
		anim =   this.transform.GetChild (0).GetChild(0).GetComponent<Animation>();
		iniStates ();
		newPos = transform.position ;
		//newForward = transform.forward;
		//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 

	}

	//
	void iniStates() {

		anim["A_Oliphant_moving"].speed = 1f;
		moving = anim["A_Oliphant_moving"];
		moving.time = 0;
		moving.enabled = true;
		anim.Sample();
		moving.enabled = false;

		anim["A_Oliphant_attack"].speed = 1f;
		attack = anim["A_Oliphant_attack"];
		attack.time = 0;
		attack.enabled = true;
		anim.Sample();
		attack.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (newPos, transform.position) > 1) {
			this.transform.GetChild (0).gameObject.SetActive (true);
			//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 
		}
		target = GameObject.FindGameObjectWithTag ("Target");
		if (target != null) {
			Vector3 dir = target.transform.position - this.transform.position;

			if (dir.magnitude < 10) {
				Debug.Log ("Atacking");
				anim.Play ("A_Oliphant_attack");
				//animationPhase = 0;
			} else {
				Debug.Log ("Elephant move animation");
				anim.Play ("A_Oliphant_moving");
			}
		}
		else {
			Debug.Log ("Elephant move animation");
			anim.Play("A_Oliphant_moving"); 

		}
	}
}
