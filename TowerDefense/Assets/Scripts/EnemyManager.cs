using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    LifeAmountManager lifeAmountManager;

    // Use this for initialization
    void Start () {

        lifeAmountManager = GameObject.FindObjectOfType<LifeAmountManager>();

        createNewWave();
	}
	
	// Update is called once per frame
	void Update () {
	    if(lifeAmountManager.remainingTime <= 0.9f)
        {
            createNewWave();
        }
	}

    void createNewWave()
    {
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
}
