using UnityEngine;
using System.Collections;
/*  Clase abstracta para definir los atributos que tiene la torrer y funciones que tiene que tener las
 *  unidades de defensa como minimo.
*/
public abstract class Tower : MonoBehaviour {
    
    public enum TowerType
    {
        UNKNOWN,
        TREBUCHET_MT,
        ROHANBARRACKS_MT,
        ORCARCHER_I,
        MERCENARYHUMAN_I,
        ARAGORN_MT,
        GANDALF_MT,
        SARUMAN_I,
        LURTZ_I,
        ORCARCHER,
        ORCWARRIOR
    }

    public struct Values
    {
        public int towerPrice;
        public TowerType type;
        public float range;
        public int strenght;
    }

    protected GameObject target;
    
    protected int level;
    protected GameObject projectile;
    
    protected bool active;
    protected abstract void getTarget();
    protected abstract void shoot();
    protected abstract void destroyTower();
    public abstract bool isActiveTower();
    public abstract void activate();
    public abstract void disable();
}
