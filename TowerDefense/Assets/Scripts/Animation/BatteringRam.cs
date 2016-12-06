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
	byte animationPhase;
	private Vector3 newPos;
	private Vector3 newForward;
	private Transform model;

	// Use this for initialization
	void Start () {
		//model = this.transform.GetChild (0);
		//anim = model.GetComponent<Animation> ();
		anim =  this.transform.GetChild (0).GetChild(0).GetComponent<Animation>();
		InitAnimation ();
		animationPhase = 0;  
		newPos = transform.position ;
		//newForward = transform.forward;
		//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 

	}

	//
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
			//transform.forward = Quaternion.Euler (0f, -90f, 0f) * newForward; 
		}
		target = GameObject.FindGameObjectWithTag("Target");
		if(target != null) {
			Vector3 dir = target.transform.position - this.transform.position;
			/*dir.y = 0f;

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.5f);
			//towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

			transform.position += transform.forward * Time.deltaTime * 7f;
			*/ 
			if (dir.magnitude < 10) {
				Debug.Log ("BATTERING1");
				anim.Play("A_Battering_attack");
				//animationPhase = 0;
			}
			else {
				//Debug.Log ("BATTERING2");  
				//Debug.Log (anim ["A_Battering_moving"].name);
				//Debug.Log (anim.name);
				anim.Play("A_Battering_moving");
			}

		}
		else {
			//Debug.Log ("BATTERING3");
			anim.Play("A_Battering_moving"); 

		}

	}

}
