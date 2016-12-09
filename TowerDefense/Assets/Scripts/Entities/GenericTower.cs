using UnityEngine;
using System.Collections;
using System;

public class GenericTower : BaseTower {

    //For Mercenary
    int idleStateHash = Animator.StringToHash("Base Layer.A_Mercenary_idle");
    int rechargeStateHash = Animator.StringToHash("Base Layer.A_Mercenary_recharge_");
    int attackStateHash = Animator.StringToHash("Base Layer.A_Mercenary_attack");

    protected override int getIdleStateHash() { return idleStateHash; }
    protected override int getAttackStateHash() { return attackStateHash; }

    // Use this for initialization
    public override void Start () {
        base.Start();

        autoRotate = true;
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

	}
}
