using UnityEngine;
using System.Collections;

public class GeneralLoop : MonoBehaviour {

    private bool contin;

    // Use this for initialization
    void Start () {
        contin = false;
	}


    // Update is called once per frame
    // Get all the enemies, increases the z position of each of them
    void Update () {
        if (contin)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z + 1);
            }
        }
    }
}
