using UnityEngine;
using System.Collections;
using System;

public class Elephant : MonoBehaviour {


	Animation elAnim;
	AnimationState moving;
	AnimationState attack;
	private Vector3 newPos;
	private bool isAttacking = false;

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
		elAnim = this.transform.GetComponent<Animation>();
		iniStates ();
		newPos = transform.position ;
		//newForward = transform.forward;
		//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 

	}

	//
	void iniStates() {


		moving = elAnim["A_Oliphant_moving"];



		attack = elAnim["A_Oliphant_attack"];

	}


	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag ("Target");
		if (Vector3.Distance (newPos, transform.position) > 1) {
			target = null;
			//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 
		}
		if (target != null) {
			Vector3 dir = target.transform.position - this.transform.position;

			if (dir.magnitude < 10) {
				Debug.Log ("Atacking");
				elAnim.Play ("A_Oliphant_attack");
				//animationPhase = 0;
			} else {
				Debug.Log ("Elephant move animation");
				elAnim.Play ("A_Oliphant_moving");
			}
		}
		else {
			Debug.Log ("Elephant move animation");
			elAnim.Play("A_Oliphant_moving"); 

		}
	}
		
}