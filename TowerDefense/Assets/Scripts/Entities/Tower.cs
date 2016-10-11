using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    private GameObject target;
    public float range = 10f;
    public string tagOfEnemy = "Enemy";
    public int life = 50;
    public int strenght = 3;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        getTarget();
        if (target == null)
            return;
        print(target);
    }

    // get de nearest enemy for shooting it or something
    void getTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagOfEnemy);
        float tmpDistance = Mathf.Infinity;
        GameObject tmpEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < tmpDistance)
            {
                tmpDistance = distanceToEnemy;
                tmpEnemy = enemy;
            }
        }
        if (tmpEnemy != null && tmpDistance <= range)
        {
            target = tmpEnemy;
        }
           // else
           // {
           //     target = null;
           // }
    }
    
    void Shoot()
    {
        // TODO
    }

    void destroy()
    {
        // TODO
    }

    
}
