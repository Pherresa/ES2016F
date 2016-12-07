using UnityEngine;
using System.Collections;
using System; 
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    LifeAmountManager lifeAmountManager;
    public Game gameValues;

    // Use this for initialization
    void Start () {

        lifeAmountManager = GameObject.FindObjectOfType<LifeAmountManager>();

        //createNewWave();
	}
	
	// Update is called once per frame
	void Update () {
	    /*if(lifeAmountManager.remainingTime <= 0.9f)
        {
            createNewWave();
        }*/
	}

    public void createNewWave()
    {


		/**
		 * For now this is the same for this two scene.
		 * TODO: Add all the model enemy (except BRAM)
		 * TODO: Thiw will dissapear!!!!!!
		 **/
		// Save game values before new wave
		gameValues = new Game(FindObjectOfType<LifeAmountManager>());
		for (int i = 0; i < 15; i++)
		{
			GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.transform.parent = transform;
			//get the thing component on your instantiated object
			AstarAI astarAI = enemy.GetComponent<AstarAI>();
			astarAI.speed = 12;
			astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
		}

		/**
		 * We deffirenciate which model goes to each scene
		 **/
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log("Active scene is '" + scene.name + "'.");// name of scene

		// Tirith scene only:
		if (scene.name == "TirithLvl1") {


			// TODO: OLiphant

			// TODO: Nazgul 

			// We generate Bettering Ram:
			generateOneBatteringRam();

		} 
		// Isengart scene only:
		else {
 
			// TODO: Orc

			// TODO: Elf

			// TODO: Hobbit

		}




		

    }

    //Destroy all the defenses and enemies in the actual round
    public void Reset()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] defenses = GameObject.FindGameObjectsWithTag("Defense");
        foreach(GameObject defense in defenses)
        {
            Destroy(defense);
        }
    }


	/**
	 * This fuction generate a Battering ram object.
	 **/
	public void generateOneBatteringRam() {

		// Creating battering ram as an enemy 
		GameObject bRamPrefab = Resources.Load ("Prefabs/attack4_BatteringRam_MT") as GameObject;
		GameObject bRam = Instantiate (bRamPrefab); 
		bRam.transform.parent = transform;
		bRam.AddComponent<BatteringRam> ();
		//get the thing component on your instantiated object
		AstarAI2 bRamAstarAI = bRam.GetComponent<AstarAI2> ();
		bRamAstarAI.speed = 10;
		bRamAstarAI.target = GameObject.FindGameObjectWithTag ("Target").transform;

	 
	}


}
