using UnityEngine;
using System.Collections;
using System;

public class Gandalf : MonoBehaviour
{


	Animation anim;
	AnimationState stateGandalftMoving;
	AnimationState stateGandalfAttacking;


	DateTime timeOnPlay;
	private Vector3 newPos;
	private Transform model;
	private bool attacking =  true;

	// Use this for initialization
	void Start()
	{

		anim = GetComponent<Animation>();
		newPos = transform.position;

	}




	// Update is called once per frame

	void Update () {
		


	}
	public void startAnimation(){
		if (attacking) {

			StartCoroutine (waitToAttack (5));
		}
		
	}

	private IEnumerator waitToAttack(float duration) {
		
		//int n_enemies = enemies.Length;
		anim = GetComponent<Animation>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		int num_enemies = enemies.Length;
		attacking = false;
	
		anim.Play ("A_Gandalf_attack");
		Debug.Log ("is attacking");
		//yield return new WaitForSeconds(duration);
		//GameObject[] batterims = GameObject.FindGameObjectsWithTag("attack4_BatteringRam_MT");


		foreach (GameObject enemy in enemies) {
			
			AstarAI2 astarAI2 = enemy.GetComponent<AstarAI2> ();
			AstarAI astarAI = enemy.GetComponent<AstarAI> ();
			if (astarAI2 != null) {
				astarAI2.speed = 3f;
			}
			if (astarAI != null) {
				astarAI.speed = 1.0f;
			} 
				
		}
		yield return new WaitForSeconds (duration);
		Destroy (this.gameObject);
		GameObject[] enemies2 = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies2) {
			float speed = 1.0f;
			if (enemy.name.Contains("Oliphant"))speed = 10;
			if (enemy.name.Contains("BatteringRam"))speed = 20;
			AstarAI2 astarAI2 = enemy.GetComponent<AstarAI2> ();
			AstarAI astarAI = enemy.GetComponent<AstarAI> ();
			if (astarAI2 != null) {
				astarAI2.speed = speed;
			}
			if (astarAI != null) {
				astarAI.speed = speed;
			} 
		}
		attacking = true;
	}

}