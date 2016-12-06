using UnityEngine;
using System.Collections;
using System;

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
      
            GameObject elephantPrefab = Resources.Load("Prefabs/attack2_Oliphant_MT") as GameObject;
			GameObject elephant = Instantiate(elephantPrefab);
			elephant.transform.parent = transform;
			elephant.AddComponent<Elephant>();
            //get the thing component on your instantiated object

			AstarAI astarAI = elephant.GetComponent<AstarAI>();
            astarAI.speed = 2f;
            astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
        


		// Creating battering ram as an enemy
		GameObject bRamPrefab = Resources.Load("Prefabs/attack4_BatteringRam_MT") as GameObject;

		// Rotation
		//GameObject bRam = (GameObject) Instantiate(bRamPrefab, bRamPrefab.transform.position, Quaternion.Euler(0, 90, 0));
		GameObject bRam = Instantiate(bRamPrefab); 

		bRam.transform.parent = transform;
		bRam.AddComponent<BatteringRam>();
		//get the thing component on your instantiated object
		AstarAI2 bRamAstarAI = bRam.GetComponent<AstarAI2>();
		bRamAstarAI.speed = 10;
		bRamAstarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
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
}
