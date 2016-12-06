﻿using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    LifeAmountManager lifeAmountManager;

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
}
