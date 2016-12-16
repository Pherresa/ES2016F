using UnityEngine;
using System.Collections;
using System;

public class OrcWarriorTower : BaseTower {

    int idleStateHash = Animator.StringToHash("Base Layer.A_OrcWarrior_idle");
    int attackStateHash = Animator.StringToHash("Base Layer.A_OrcWarrior_attack");

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
}
