using UnityEngine;
using System.Collections;
using System;

public class OrcArcherTower : BaseTower {

    int idleStateHash = Animator.StringToHash("Base Layer.A_OrcArcher_idle");
    int rechargeStateHash = Animator.StringToHash("Base Layer.A_OrcArcher_recharge");
    int attackStateHash = Animator.StringToHash("Base Layer.A_OrcArcher_attack");

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
        return Quaternion.Euler(0f, -90f, 0f);
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
