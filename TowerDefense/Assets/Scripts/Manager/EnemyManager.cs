using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    GameManager lifeAmountManager;
    public Game gameValues;

    // Use this for initialization
    void Start () {

        lifeAmountManager = GameObject.FindObjectOfType<GameManager>();

        //createNewWave();
	}
	
	// Update is called once per frame
	void Update () {
	    /*if(lifeAmountManager.remainingTime <= 0.9f)
        {
            createNewWave();
        }*/
	}

    public void createNewWaveIsengard(int round) {
        System.Random dado = new System.Random();
        gameValues = new Game(FindObjectOfType<GameManager>());
        GameObject enemyPrefab1 = (GameObject)Resources.Load("Prefabs/attack3_Elf_I");
        GameObject enemyPrefab2 = (GameObject)Resources.Load("Prefabs/attack1_Ent_I");
        GameObject enemyPrefab3 = (GameObject)Resources.Load("Prefabs/attack4_Hobbit_I");

        for (int i = 0; i < 10; i++)
        {
            int n = dado.Next(1,4);
            //GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
            GameObject enemy;//= Instantiate(enemyPrefab);
            if (n == 1) {
                enemy = Instantiate(enemyPrefab1);
            } else if (n ==2) {
                enemy = Instantiate(enemyPrefab2);
            }
            else {
                enemy = Instantiate(enemyPrefab3);
            }
            enemy.transform.parent = transform;
            enemy.transform.position = transform.position;
            //get the thing component on your instantiated object
            AstarAI astarAI = enemy.GetComponent<AstarAI>();
            astarAI.speed = enemy.GetComponent<Enemy>().getValues().speed;
            astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
        }
    }

    public void createNewWaveMinasTirith() {
        System.Random dado = new System.Random();
        gameValues = new Game(FindObjectOfType<GameManager>());
        GameObject enemyPrefab1 = (GameObject)Resources.Load("Prefabs/attack1_Orc_MT");
        GameObject enemyPrefab2 = (GameObject)Resources.Load("Prefabs/attack2_Oliphant_MT");
        GameObject enemyPrefab3 = (GameObject)Resources.Load("Prefabs/attack4_BatteringRam_MT");

        for (int i = 0; i < 10; i++)
        {
            int n = dado.Next(1, 4);
            //GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
            GameObject enemy;// = Instantiate(enemyPrefab);
            if (n == 1)
            {
                enemy = Instantiate(enemyPrefab1);
            }
            else if (n == 2)
            {
                enemy = Instantiate(enemyPrefab2);
            }
            else
            {
                enemy = Instantiate(enemyPrefab3);
            }
            enemy.transform.parent = transform;
            enemy.transform.position = transform.position;
            //get the thing component on your instantiated object
            AstarAI astarAI = enemy.GetComponent<AstarAI>();
            astarAI.speed = enemy.GetComponent<Enemy>().getValues().speed;
            astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
        }
    }

    public void createNewWave()
    {
 

		/**
		 * For now this is the same for this two scene.
		 * TODO: Add all the model enemy (except BRAM)
		 * TODO: Thiw will dissapear!!!!!!
		 **/
		// Save game values before new wave
		gameValues = new Game (FindObjectOfType<GameManager> ());
		for (int i = 0; i < 15; i++) {
			GameObject enemyPrefab = Resources.Load ("Prefabs/Enemy") as GameObject;
			GameObject enemy = Instantiate (enemyPrefab);
			enemy.transform.parent = transform;
            enemy.transform.position = transform.position;
			//get the thing component on your instantiated object
			AstarAI astarAI = enemy.GetComponent<AstarAI> ();
			astarAI.speed = enemy.GetComponent<Enemy>().getValues().speed;
			astarAI.target = GameObject.FindGameObjectWithTag ("Target").transform;
		}

		/**
		 * We deffirenciate which model goes to each scene
		 **/
		Scene scene = SceneManager.GetActiveScene ();
		Debug.Log ("Active scene is '" + scene.name + "'.");// name of scene

		// Tirith scene only:
		if (scene.name == "TirithLvl1") {


			// TODO: OLiphant
            
			// We generate Bettering Ram:
			generateOneBatteringRam ();

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
