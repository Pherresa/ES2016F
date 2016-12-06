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
        // Save game values before new wave
        gameValues = new Game(FindObjectOfType<LifeAmountManager>());
        for (int i = 0; i < 15; i++)
        {
            GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = transform;
            //get the thing component on your instantiated object
            AstarAI astarAI = enemy.GetComponent<AstarAI>();
            astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
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
}
