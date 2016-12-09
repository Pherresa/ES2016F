using UnityEngine;
using System.Collections;
using System;

public class LurtzTower : BaseTower {

    //For Mercenary
    int idleStateHash = Animator.StringToHash("Base Layer.A_Lurtz_idle_");
    int rechargeStateHash = Animator.StringToHash("Base Layer.A_Lurtz_recharge");
    int attackStateHash = Animator.StringToHash("Base Layer.A_Lurtz_attack");

    protected override int getIdleStateHash() { return idleStateHash; }
    protected override int getAttackStateHash() { return attackStateHash; }

    // Use this for initialization
    public override void Start () {
        base.Start();

        autoRotate = true;
	}

    protected override Quaternion getFixedRotation()
    {
        return Quaternion.Euler(0f, 180f, 0f);
    }

    protected override Quaternion getFixedProjectileRotation()
    {
        return Quaternion.Euler(0f, 0f, 0f);
    }
    
    protected override float getProjectileDuration()
    {
        return 5f; //secs
    }

    protected override float getProjectileSpeed()
    {
        return 25f;
    }
}
