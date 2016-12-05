using UnityEngine;
using System.Collections;
using System;

public class RohanHorse : MonoBehaviour {

 
	Animation anim;
	AnimationState stateRohanHorseRunning;
	AnimationState stateRohanHorseAttacking; 
 	
	GameObject target;
	public Vector3 center;
	public int range = 60;

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
		
		anim ["A_RHorse_run"].speed = 2f;
		stateRohanHorseRunning = anim ["A_RHorse_run"];
		stateRohanHorseRunning.time = 0;
		stateRohanHorseRunning.enabled = true;
		anim.Sample ();
		stateRohanHorseRunning.enabled = false;

		anim ["A_RHorse_attack"].speed = 1f;
		stateRohanHorseAttacking = anim ["A_RHorse_attack"];
		stateRohanHorseAttacking.time = 0;
		stateRohanHorseAttacking.enabled = true;
		anim.Sample ();
		stateRohanHorseAttacking.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		target = getTarget ();
		if(target != null) {
			Vector3 dir = target.transform.position - this.transform.position;
			dir.y = 0f;

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.6f);
			//towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

			transform.position += transform.forward * Time.deltaTime * 7f;
			if (dir.magnitude < 5) {
				anim.Play("A_RHorse_attack");
				//animationPhase = 0;
			}
			else {
				anim.Play("A_RHorse_run");
			}

		}
		else {
			anim.Play("A_RHorse_run"); 

		}
	
	}


	/**private void checkPhaseAnim()
	{ 
		if (animationPhase == 1)
		{
			timeOnPlay = DateTime.Now;
			anim.Play("A_Trebuchet_attack");
			animationPhase = 2;
		}*/

				

	//  
	private GameObject getTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject auxTarget=null;
		float tmpDistanceCenter = Mathf.Infinity;
		float tmpDistanceRH = Mathf.Infinity;
		GameObject tmpEnemy = null;
		float distanceBetweenCenterEnemy =  Mathf.Infinity; 
		float distanceBetweenRHEnemy = Mathf.Infinity;
		//bool isNotCollisiongWOR = isNotCollisioningWithOtherRohan (3);
		Debug.Log("CENTER");
		Debug.Log (center);
		float auxDistance = Mathf.Infinity;
		foreach (GameObject enemy in enemies)
		{ 
			distanceBetweenCenterEnemy = Vector3.Distance(center, enemy.transform.position);
			distanceBetweenRHEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);

			if (distanceBetweenCenterEnemy < tmpDistanceCenter)
			//if(distanceBetweenCenterEnemy+distanceBetweenRHEnemy < auxDistance) 
			{
				auxDistance = distanceBetweenCenterEnemy + distanceBetweenRHEnemy;
				tmpDistanceCenter = distanceBetweenCenterEnemy;
				tmpDistanceRH = distanceBetweenRHEnemy;
				if (distanceBetweenCenterEnemy <= range) {
					tmpEnemy = enemy; 
				}
			}
		}
		if (tmpEnemy != null && tmpDistanceCenter <= range)
		{
			auxTarget = tmpEnemy;
		}
		return auxTarget;
	}

	/*
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
	}*/
		

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
		Debug.Log (coll.gameObject.name);
		if (coll.gameObject.name.Split('(')[0] == "Enemy") {
			Enemy ene = coll.GetComponent<Enemy> ();
			//Destroy(this.gameObject);
			ene.playSound (ene.soundAttacked);
		
			//print(col.gameObject.name);
			ene.life -= 5f;
		}
		/*
		if (coll.gameObject.name.Split('(')[0] == "defense2P_RohanHorse_MT") {
			Vector3 newPosRH = coll.gameObject.transform.position;
			newPosRH.x += 0.1f; 
			newPosRH.z += 0.1f; 
			coll.gameObject.transform.position= newPosRH;

		}*/
	}
}
