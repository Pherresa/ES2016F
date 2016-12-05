using UnityEngine;
using System.Collections;
using System;

public class RohanHorse : MonoBehaviour {

 
	Animation anim;
	AnimationState stateRohanHorseRunning;
	AnimationState stateRohanHorseAttacking; 
 	
	GameObject target;
	public Vector3 center;
	public int range = 40;

	DateTime timeOnPlay;
	byte animationPhase;
	private Vector3 newPos;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animation>();
		InitAnimation ();
		animationPhase = 0; 
		/*
		newPos = this.transform.position;
		newPos.x += 5;
		this.transform.position = newPos;*/
	}

	//
	void InitAnimation() {
		
		anim ["A_RHorse_run"].speed = 1f;
		stateRohanHorseRunning = anim ["A_RHorse_run"];
		stateRohanHorseRunning.time = 0;
		stateRohanHorseRunning.enabled = true;
		anim.Sample ();
		//stateRohanHorseRunning.enabled = false;

		anim ["A_RHorse_attack"].speed = 1.5f;
		stateRohanHorseAttacking = anim ["A_RHorse_attack"];
		stateRohanHorseAttacking.time = 0;
		stateRohanHorseAttacking.enabled = true;
		anim.Sample ();
		stateRohanHorseAttacking.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		checkTarget ();
		if(target != null) {
			Vector3 dir = target.transform.position - this.transform.position;
			dir.y = 0f;

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.1f);
			//towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

			transform.position += transform.forward * Time.deltaTime * 8f;
			if (dir.magnitude < 4) {
				anim.Play("A_RHorse_attack");
				animationPhase = 0;
			}
			else {
				anim.Play("A_RHorse_run");
				animationPhase = 1;
			}

		}
	
	}


	/*private void checkPhaseAnim()
	{ 
		if (animationPhase == 1)
		{
			timeOnPlay = DateTime.Now;
			anim.Play("A_Trebuchet_attack");
			animationPhase = 2;
		}*/

				

	//  
	private void checkTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		float tmpDistance = Mathf.Infinity;
		GameObject tmpEnemy = null;
		float distanceToEnemy;
		bool isNotCollisiongWOR = isNotCollisioningWithOtherRohan (3);
		foreach (GameObject enemy in enemies)
		{ 
			distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < tmpDistance)
			{
				tmpDistance = distanceToEnemy;
				tmpEnemy = enemy; 
			}
		}
		if (tmpEnemy != null && tmpDistance <= range)
		{
			target = tmpEnemy;
		}
		else
		{
			target = null; 
		} 
	}


	bool isNotCollisioningWithOtherRohan(int minDist) {
		bool isNotCollisioning = true;
		float distanceToRH =  Mathf.Infinity;
		GameObject[] rohanHorses = GameObject.FindGameObjectsWithTag("rohanHorse");
		int iter = 0;
		GameObject rH = null;
		int rohanHorsesLength = rohanHorses.Length;
		if (rohanHorsesLength > 1 && iter < rohanHorsesLength) {
			while(isNotCollisioning) {
				rH = rohanHorses[iter];
				distanceToRH = Vector3.Distance(transform.position, rH.transform.position);
				if (distanceToRH < minDist)
				{ 
					isNotCollisioning = false;
				}
				iter++;
			}
		}

		return isNotCollisioning;
	}
		

	// 
	void OnCollisionEnter(Collision col)
	{
		//TODO
		/*Enemy enemy = (Enemy)target;
		enemy.life -= 40;*/
		Debug.Log("ROHAN HORSE COLISIONA!");

	}

	void OnTriggerEnter(Collider coll) {

		Debug.Log ("PROJECTILE");
		if (coll.gameObject.name.Split('(')[0] == "Enemy") {
			Enemy ene = coll.GetComponent<Enemy> ();
			//Destroy(this.gameObject);
			ene.playSound (ene.soundAttacked);
		
			//print(col.gameObject.name);
			ene.life -= 50f;
		}
	}
}
