using UnityEngine;
using System.Collections;
using System;

/**
 * This class implements the animation of rohan horses
 **/
public class RohanHorse : MonoBehaviour {

    Action_Defense.Values val;

    Enemy_Values_Gene generation;

	Animation anim;
	AnimationState stateRohanHorseRunning;
	AnimationState stateRohanHorseAttacking; 
 	
	GameObject target;
	public Vector3 center;
    public int range;// = Enemy_Constants.T_RANGE_MEDIUM/2;

	DateTime timeOnPlay;
	byte animationPhase;
	private Vector3 newPos;

    void Awake() {
        generation = new Enemy_Values_Gene();
        val.type = Tower.TowerType.ROHANBARRACKS_MT;
        generation.asig_values_tower(ref val);
        range = (int)val.range;
    }

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animation>();
		InitAnimation ();
		animationPhase = 0;  
	}

	// We initiate the rohan horses animation 
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

			//float vec = Terrain.activeTerrain.SampleHeight(this.transform.position);
			dir.y = 0f;

			//Debug.Log ("Is path? '" + vec + "'.");
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.5f);
			 
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


	/**
	 * This support function obtain the nearest enemy of the rohan horse that's
	 *  inside the range circle.
	 **/ 
	private GameObject getTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject auxTarget=null;

		float tmpDistanceRH = Mathf.Infinity;

		float distanceBetweenCenterEnemy =  Mathf.Infinity; 
		float distanceBetweenRHEnemy = Mathf.Infinity;
		//bool isNotCollisiongWOR = isNotCollisioningWithOtherRohan (3);
	
		Debug.Log (center);
		foreach (GameObject enemy in enemies) { 
			// 1rst we see if the enemy is inside the circle operation:
			distanceBetweenCenterEnemy = Vector3.Distance (center, enemy.transform.position);
			if (distanceBetweenCenterEnemy <= range) {
				// 2nd we look for the nearest enemy
				distanceBetweenRHEnemy = Vector3.Distance (this.transform.position, enemy.transform.position);
				if (distanceBetweenRHEnemy < tmpDistanceRH) {
					tmpDistanceRH = distanceBetweenRHEnemy;
					auxTarget = enemy;
				}
			}
		}

		return auxTarget;
	}

		

	// 
	void OnCollisionEnter(Collision col)
	{
		Debug.Log("ROHAN HORSE COLISIONA!");

	}

	// Cillision treatment.
	void OnTriggerEnter(Collider coll) {

		// Rohan horse damage on the enemy.
		if (coll.gameObject.name.Split('(')[0] == "Enemy") {
			Enemy ene = coll.GetComponent<Enemy> ();
			//Enemy ene = coll.gameObject.transform.GetChild (0).GetChild(0).GetComponent<Enemy> ();
			Debug.Log ("Enemy name is '" + ene.name + "'.");
			ene.playSound (ene.soundSword);

            ene.life -= val.strenght; // Enemy_Constants.T_ATTACK_LITTLE;
		}


		// We separate horses if they intersect
		if (coll.gameObject.name.Split('(')[0] == "defense2P_RohanHorse_MT") {
			
			Vector3 newPosRH;
			if(Vector3.Distance(this.transform.position, coll.gameObject.transform.position) <= 3f) {
				newPosRH = this.transform.position;
				newPosRH.x += 0.3f; //newPosRH.z += 0.5f;
				this.transform.position= newPosRH;
			}

		}
	}
}
