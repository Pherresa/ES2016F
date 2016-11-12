using UnityEngine;
using System.Collections;
using System;

public class Action_Defense : Tower
{

    private float timer = 0.7f;

    // Use this for initialization
    void Start()
    {
        iniStates();
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();
        if (target == null)
            return;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.7f;
            Shoot();
        }
    }

    void iniStates()
    {
        range = 30f;
        strenght = 1;
        getTarget();
    }

    protected override void DestroyTower()
    {
        Destroy(this.gameObject);
    }

    protected override void Shoot()
    {
        if (target != null)
        {
            float distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);
            if (distanceToEnemy < range)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                cube.AddComponent<Rigidbody>();
                Vector3 tmp = this.transform.position;
                tmp.y += 2;
                cube.transform.position = tmp;
                cube.transform.localScale = new Vector3(1f, 1f, 1f);
                cube.AddComponent<ShootingMove>();
                cube.GetComponent<ShootingMove>().target = target;
            }
        }
    }

    protected override void getTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
         else
         {
             target = null;
         }
    }
}

