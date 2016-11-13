using UnityEngine;
using System.Collections;
using System;

public class Action_Defense : Tower
{
    //Animation animation;
    private float timer = 1f;

    void Start()
    {
        iniStates();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            getTarget();
            if (target == null)
                return;
            timer = 1f;
            //anim.Play();
            // if (!anim.isPlaying){
            if (type!=0)
            {
                Shoot();
            }
           // }
        }
    }

    void iniStates()
    {
        range = 30f;
        strenght = 1;
        getTarget();
        getTypeOfDefense();
    }

    private void getTypeOfDefense()
    {
        String name = this.gameObject.name.Split('(')[0];
        print(name);
        if (name == "defense1_Trebuchet_MT")
        {
            type = 1;
        }
        if (name == "defense2_RohanBarracks_MT")
        {
            type = 2;
        }
        if (name == "defense2_OrcArcher_I")
        {
            type = 3;
        }
        if (name == "defense3_MercenaryHuman_I")
        {
            type = 4;
        }
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
                shootProjectile();
            }
        }
    }

    private GameObject shootProjectile()
    {
        GameObject pro = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pro.AddComponent<Rigidbody>();
        Vector3 tmp = this.transform.position;
        tmp.y += 2;
        pro.transform.position = tmp;
        pro.transform.localScale = new Vector3(1f, 1f, 1f);
        pro.AddComponent<ShootingMove>();
        pro.GetComponent<ShootingMove>().target = target;
        if (type == 1)
        {
            pro.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (type == 2)
        {
            pro.GetComponent<Renderer>().material.color = Color.green;
        }
        if (type == 3)
        {
            pro.GetComponent<Renderer>().material.color = Color.red;
        }
        if (type == 4)
        {
            pro.GetComponent<Renderer>().material.color = Color.yellow;
        }
        
        return projectile;
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

