using UnityEngine;
using System.Collections;
/*  Clase abstracta para definir los atributos que tiene la torrer y funciones que tiene que tener las
 *  unidades de defensa como minimo.
*/
public abstract class Defense : MonoBehaviour {
    protected GameObject target;
    protected GameObject projectile;
    private GameObject explosion;

    protected float range;

    protected int life;
    protected int level;
    protected int type;

    protected bool active;
    
    protected abstract void getTarget();
    protected abstract void shoot();
    protected abstract void destroyTower();
    protected abstract void checkLife();

    public abstract bool isActiveTower();
    public abstract void activate();
    public abstract void disable();
    public abstract void onCollisionEnter(Collision col);
    

}
