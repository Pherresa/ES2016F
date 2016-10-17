using UnityEngine;
using System.Collections;
using System;

public class MainTower : Tower
{

    private float timer = 2f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
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
            timer = 2f;
            Shoot();
        }
    }

    void iniStates()
    {
        range = 13f;
        life = 200;
        strenght = 7;
        getTarget();
    }


    protected override void Shoot()
    {
        if (target != null)
        {
            float distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);
            if (distanceToEnemy < range)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                cube.GetComponent<Renderer>().material.color = Color.red;
                //cube.AddComponent<Rigidbody>();
                cube.transform.position = this.transform.position;
                cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cube.GetComponent<Collider>().isTrigger = true;
                cube.AddComponent<ShootingMove>();
                cube.GetComponent<ShootingMove>().postarget = target.transform.position;
            }
        }
    }

    protected override void DestroyTower()
    {
        Destroy(this.gameObject);
    }
    protected  void decreLife()
    {
        life = life - 1;
    }
}