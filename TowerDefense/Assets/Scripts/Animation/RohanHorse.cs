using UnityEngine;
using System.Collections;
using System;

public class RohanHorse : MonoBehaviour {

 
	Animation anim;
	AnimationState stateRohanHorseRunning;
	AnimationState stateRohanHorseAttacking; 
 	
	GameObject target;
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
		stateRohanHorseRunning.enabled = false;

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

			transform.position += transform.forward * Time.deltaTime * 5f;
			if (dir.magnitude < 2) {
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
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
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


	// 
	void OnCollisionEnter(Collision col)
	{
		//TODO
		/*Enemy enemy = (Enemy)target;
		enemy.life -= 40;*/
		Debug.Log("ROHAN HORSE COLISIONA!");

	}
}
