using UnityEngine;
using System.Collections;
using System;

/**
 * This class implements the animation of rohan horses
 **/
public class DefenseWarrior : MonoBehaviour {

	public enum WarriorType
	{
		UNKNOWN,
		ROHAN_HORSE_MT,
		GHOST_WARRIOR_MT,
		ARAGORN_WARRIOR_MT
	}

	private WarriorType warriorType;

    Action_Defense.Values val;

    Enemy_Values_Gene generation;

	Animation anim;
	AnimationState stateRunning;
	AnimationState stateAttacking; 
 	
	GameObject target;
	public Vector3 center;
    public int range;// = Enemy_Constants.T_RANGE_MEDIUM/2;
	public int vel;
	private float durationAnim;
	private int distActivateAnim;

	DateTime timeOnPlay; 
	private Vector3 newPos; 

    void Awake() { 
		assignWarriorType ();
        generation = new Enemy_Values_Gene(); 
        generation.asig_values_tower(ref val);
        range = (int)val.range;
		 
    }


	// Get warrior tyepe and  tower defense type 
	private void assignWarriorType ()
	{ 
		String name = this.gameObject.name.Split ('(') [0];
		if (name == "defense2P_RohanHorse_MT") {
			warriorType = WarriorType.ROHAN_HORSE_MT;
			val.type = Tower.TowerType.ROHANBARRACKS_MT;
			vel = 7; 
			durationAnim = 1.0f;
			distActivateAnim = 10;

		} else if (name == "defender4_Aragorn_MT") {
			warriorType = WarriorType.ARAGORN_WARRIOR_MT;
			val.type = Tower.TowerType.ARAGORN_MT;
			vel = 5; 
			durationAnim = 1.0f;
			distActivateAnim = 8;

		} else if (name == "defense3P_Ghost_MT") {
			warriorType = WarriorType.GHOST_WARRIOR_MT;
			val.type = Tower.TowerType.GHOSTSHIP_MT;
			vel = 7;
			durationAnim = 1.0f;
			distActivateAnim = 10;

		} else {
			warriorType = WarriorType.UNKNOWN;
			vel = 0;
			durationAnim = 0.0f;
			distActivateAnim = 0;
		}
		 
	}


 


	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animation>();
		InitAnimation ();  
	}


	
	// Update is called once per frame
	void Update () {

		target = getTarget ();
		if(target != null) {
			Vector3 dir = target.transform.position - this.transform.position; 
			dir.y = 0f;
 
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.5f);
			 
			transform.position += transform.forward * Time.deltaTime * vel;
			if (dir.magnitude < distActivateAnim) {
				timeOnPlay = DateTime.Now;
				playAnimation ("attack"); 
			}
			else if ((DateTime.Now - timeOnPlay).Seconds > durationAnim) {
				playAnimation ("run"); 
			} 
		}
		else {
			Vector3 dir = this.center - this.transform.position; 
			dir.y = 0f;
			if (dir.magnitude < 0.3f) {
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (dir), 0.5f);

				transform.position += transform.forward * Time.deltaTime * vel;
				playAnimation ("run"); 
			}


		}
	
	}


	// Play the animation name
	private void playAnimation(String nameAnimation) 
	{
		switch (warriorType) 
		{
			case WarriorType.ROHAN_HORSE_MT:
				anim.Play ("A_RHorse_" + nameAnimation); 
				break;
			case WarriorType.GHOST_WARRIOR_MT:
				anim.Play ("A_Ghost_"+nameAnimation); 	
				break;				
			case WarriorType.ARAGORN_WARRIOR_MT:
				anim.Play ("A_Aragorn_"+nameAnimation); 
				break;
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
		float tmpDistanceCenter = Mathf.Infinity;
		float tmpDistanceRH = Mathf.Infinity;
		GameObject tmpEnemy = null;
		float distanceBetweenCenterEnemy =  Mathf.Infinity; 
		float distanceBetweenRHEnemy = Mathf.Infinity;
		//bool isNotCollisiongWOR = isNotCollisioningWithOtherRohan (3);
		//Debug.Log("CENTER");
		//Debug.Log (center);
		float auxDistance = Mathf.Infinity;
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
		Debug.Log ("Enemy name dfads");

		Debug.Log (coll.gameObject.name);
		// Rohan horse damage on the enemy.
		//if (coll.gameObject.name.Split('(')[0] == "Enemy") {
		if (coll.gameObject.transform.CompareTag("Enemy")) {	
			Enemy ene = coll.GetComponent<Enemy> ();
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


	// We initiate the rohan horses animation 
	void InitAnimation() {

		switch (warriorType)
		{
		case WarriorType.ROHAN_HORSE_MT:
			anim ["A_RHorse_run"].speed = 2f;
			stateRunning = anim ["A_RHorse_run"];
			stateRunning.time = 0;
			stateRunning.enabled = true;
			anim.Sample ();
			stateRunning.enabled = false;

			anim ["A_RHorse_attack"].speed = 1f;
			stateAttacking = anim ["A_RHorse_attack"];
			stateAttacking.time = 0;
			stateAttacking.enabled = true;
			anim.Sample ();
			stateAttacking.enabled = false;
			break;

		case WarriorType.GHOST_WARRIOR_MT:
			anim ["A_Ghost_run"].speed = 2f;
			stateRunning = anim ["A_Ghost_run"];
			stateRunning.time = 0;
			stateRunning.enabled = true;
			anim.Sample ();
			stateRunning.enabled = false;

			anim ["A_Ghost_attack"].speed = 1f;
			stateAttacking = anim ["A_Ghost_attack"];
			stateAttacking.time = 0;
			stateAttacking.enabled = true;
			anim.Sample ();
			stateAttacking.enabled = false;
			break;

		case WarriorType.ARAGORN_WARRIOR_MT:
			anim ["A_Aragorn_run"].speed = 2f;
			stateRunning = anim ["A_Aragorn_run"];
			stateRunning.time = 0;
			stateRunning.enabled = true;
			anim.Sample ();
			stateRunning.enabled = false;

			anim ["A_Aragorn_attack"].speed = 2f;
			stateAttacking = anim ["A_Aragorn_attack"];
			stateAttacking.time = 0;
			stateAttacking.enabled = true;
			anim.Sample ();
			stateAttacking.enabled = false;
			break;  
		}
	}

}
