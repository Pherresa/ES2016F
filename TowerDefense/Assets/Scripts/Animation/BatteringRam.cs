using UnityEngine;
using System.Collections;
using System;

public class BatteringRam : MonoBehaviour {


	Animation anim;
	AnimationState stateBRamMoving;
	AnimationState stateBRamAttacking; 

	GameObject target;
	public Vector3 center;
	public int range = 60;

	DateTime timeOnPlay; 
	private Vector3 newPos;
	private Vector3 newForward;
	private Transform model;

	// Use this for initialization
	void Start () { 
		anim =  this.transform.GetChild (0).GetChild(0).GetComponent<Animation>();
		InitAnimation (); 
		newPos = transform.position ; 
		target = GameObject.FindGameObjectWithTag("Target");

	}

	// Initiate Animation
	void InitAnimation() {

		anim ["A_Battering_moving"].speed = 1f;
		stateBRamMoving = anim ["A_Battering_moving"];
		stateBRamMoving.time = 0;
		stateBRamMoving.enabled = true;
		anim.Sample ();
		stateBRamMoving.enabled = false;

		anim ["A_Battering_attack"].speed = 2f;
		stateBRamAttacking = anim ["A_Battering_attack"];
		stateBRamAttacking.time = 0;
		stateBRamAttacking.enabled = true;
		anim.Sample ();
		stateBRamAttacking.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (newPos, transform.position) > 1) {
			this.transform.GetChild (0).gameObject.SetActive (true);  
		}

		if(target != null) {
			Vector3 dir = target.transform.position - this.transform.position; 
			if (dir.magnitude < 10) {
				Debug.Log ("BATTERING1");
				anim.Play("A_Battering_attack"); 
			}
			else { 
				anim.Play("A_Battering_moving");
			}

		}
		else { 
			anim.Play("A_Battering_moving"); 

		}

	}

}
