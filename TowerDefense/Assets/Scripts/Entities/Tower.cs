using UnityEngine;
using System.Collections;
/*  Clase abstracta para definir los atributos que tiene la torrer y funciones que tiene que tener las
 *  unidades de defensa como minimo.
*/
public abstract class Tower : MonoBehaviour {
    protected GameObject target;
    public float range;
    protected int strenght;
    protected int level;
    protected GameObject projectile;
    protected int type;
    protected bool active;
    protected abstract void getTarget();
    protected abstract void shoot();
    protected abstract void destroyTower();
    public abstract bool isActiveTower();
    public abstract void activate();
    public abstract void disable();
}
