using UnityEngine;
using System.Collections;
using System;

public class EntEnemy : BaseEnemy {

    // Use this for initialization
    public override void Start () {
        base.Start();

        autoRotate = false;
	}

   

    protected override Quaternion getFixedProjectileRotation()
    {
        return Quaternion.Euler(90f, 0f, 0f);
    }

    protected override float getProjectileDuration()
    {
        return 5f; //secs
    }

    protected override float getProjectileSpeed()
    {
        return 25f;
    }

    protected override int getNumAttacks()
    {
        return 3;
    }
}
