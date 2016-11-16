using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 100; i++)
        {
            GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = transform;
            //get the thing component on your instantiated object
            AstarAI astarAI = enemy.GetComponent<AstarAI>();
            astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
