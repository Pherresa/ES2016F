using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour {

    protected GameObject target;
    protected float range;
    protected int strenght;
    protected int level;
    protected GameObject projectile;
    protected int type = 0;
    protected abstract void getTarget();
    protected abstract void Shoot();
    protected abstract void DestroyTower();
}
