using UnityEngine;
using System.Collections;
using System;

public class Tower : Defense {

    private GameObject[] enemies;
    private GameObject finish;
    // Use this for initialization
    void Start()
    {
        life = 200;
        finish = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void activate()
    {
        throw new NotImplementedException();
    }

    public override void disable()
    {
        throw new NotImplementedException();
    }

    public override bool isActiveTower()
    {
        throw new NotImplementedException();
    }

    public override void onCollisionEnter(Collision col)
    {
        throw new NotImplementedException();
    }

    protected override void checkLife()
    {
        if(life <= 0)
        {
            //TODO: IMPLEMENT WHAT HAPPENS WHEN USER LOOSE.
            Debug.Log("GAME OVER YOU LOST");
        }
    }

    protected override void destroyTower()
    {
        throw new NotImplementedException();
    }

    protected override void getTarget()
    {
        throw new NotImplementedException();
    }

    protected override void shoot()
    {
        throw new NotImplementedException();
    }

    
}
