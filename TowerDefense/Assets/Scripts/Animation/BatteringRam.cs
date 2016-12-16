using UnityEngine;
using System.Collections;
using System;

public class BatteringRam : MonoBehaviour {


	Animation anim;
	AnimationState stateBRamMoving;
	AnimationState stateBRamAttacking; 

	GameObject target; 
 
	private Vector3 newPos;  
	private Vector3 target_animation;

	// Use this for initialization
	void Start () { 
		anim =  this.transform.GetChild (0).GetChild(0).GetComponent<Animation>();
		InitAnimation (); 
		newPos = transform.position ; 
		target = GameObject.FindGameObjectWithTag("Target"); 

		//target_animation = new Vector3 (-60.89f, 0f, 64.25f);
		target_animation = new Vector3 (-76f, 0f, 79f);
 
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
			Vector3 dir = target_animation - this.transform.position; 
			//Debug.Log ("BRAM");
			//Debug.Log (target_animation.ToString());
			//Debug.Log (this.transform.position.ToString());
			//Debug.Log (dir.magnitude);
			if (dir.magnitude < 30) {
				//Debug.Log ("BATTERING1");
				anim.Play("A_Battering_attack");
				//moveBRamNow (dir);
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
