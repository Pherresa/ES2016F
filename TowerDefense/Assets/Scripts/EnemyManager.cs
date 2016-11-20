using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 100; i++)
        {

            GameObject enemyPrefab = Resources.Load("Prefabs/attack1_Orc_MT") as GameObject;
            GameObject enemyOrc = Instantiate(enemyPrefab);
            enemyOrc.transform.parent = transform;
            enemyOrc.tag = "Enemy";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
