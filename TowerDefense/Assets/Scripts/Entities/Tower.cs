using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour {

    protected GameObject target;
    protected float range;
    protected string tagOfEnemy = "Enemy";
    protected int life;
    protected int strenght;

    

    // get de nearest enemy for shooting it or something
    protected void getTarget()
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

    protected abstract void Shoot();
    protected abstract void Destroy();

}
