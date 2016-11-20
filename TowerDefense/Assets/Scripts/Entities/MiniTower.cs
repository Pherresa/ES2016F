using UnityEngine;
using System.Collections;

public class MiniTower : Tower
{

    private float timer = 0.7f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
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
        range = (float)Enemy_Values_Gene.m_little_tower("r");
        life = 20;//Enemy_Values_Gene.m_little_tower("l");
        strenght = Enemy_Values_Gene.m_little_tower("a");
        getTarget();
    }

    protected  void decreLife()
    {
        life = life - 1;
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
                cube.GetComponent<Renderer>().material.color = Color.green;
                //cube.AddComponent<Rigidbody>();
                cube.transform.position = this.transform.position;
                cube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                cube.AddComponent<ShootingMove>();
                cube.GetComponent<ShootingMove>().postarget = target.transform.position;
            }
        }
    }
}
