using UnityEngine;
using System.Collections;
/*  Clase abstracta para definir los atributos que tiene la torrer y funciones que tiene que tener las
 *  unidades de defensa como minimo.
*/
public abstract class BaseTower : MonoBehaviour {
    
    public enum TowerType
    {
        UNKNOWN,
        TREBUCHET_MT,
        ROHANBARRACKS_MT,
        ORCARCHER_I,
        MERCENARYHUMAN_I
    }

    //FLAGS
    protected bool autoRotate = true;
    protected int shootStateHash = Animator.StringToHash("Shoot");

    //Internal vars
    protected bool active;
    protected int level;
    protected Animator animator;
    protected bool isShooting = false;

    //External vars
    public TowerType type = TowerType.UNKNOWN;
    public float range;
    public int strenght;

    //Common vars
    protected GameObject target;
    public GameObject projectile;

    protected abstract int getIdleStateHash();
    protected abstract int getAttackStateHash();
    protected abstract Quaternion getFixedRotation();
    protected abstract Quaternion getFixedProjectileRotation();
    protected abstract float getProjectileDuration();
    protected abstract float getProjectileSpeed();

    public void destroyTower() { Destroy(gameObject); }
    public bool isActiveTower() { return active; }
    public void activate() { active = true; }
    public void disable() { active = false; }
    public int getCurrentAnimationStateHash() { return animator != null ? animator.GetCurrentAnimatorStateInfo(0).fullPathHash : -1; }
    public int getNextAnimationStateHash() { return animator != null ? animator.GetNextAnimatorStateInfo(0).fullPathHash : -1; }

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    {
        getTarget();
        shoot();
        checkAnimationState();
    }

    public virtual void Update()
    {
        if (autoRotate) rotate();
    }

    public void shoot()
    {
        if (target != null && getCurrentAnimationStateHash() == getIdleStateHash())
        {
            projectile.SetActive(true);
            startShootAnimation();
            isShooting = false;
        } else if (target == null && getCurrentAnimationStateHash() == getIdleStateHash())
        {
            projectile.SetActive(false);
        }
    }

    private void checkAnimationState()
    {
        if (!isShooting && animator.IsInTransition(0) && getCurrentAnimationStateHash() == getAttackStateHash())
        {
            generateProjectile();
            isShooting = true;
        }
    }

    protected void startShootAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger(shootStateHash);
        }
    }

    protected void generateProjectile()
    {
        if(projectile != null)
        {
            GameObject projectileInstance = Instantiate(projectile);
            //projectileInstance.transform.parent = transform;
            projectileInstance.transform.localScale = 
                new Vector3(transform.localScale.x * projectile.transform.localScale.x,
                transform.localScale.y * projectile.transform.localScale.y,
                transform.localScale.z * projectile.transform.localScale.z);
            projectileInstance.transform.position = projectile.transform.position;// + new Vector3(0f, 3f, 0f);
            projectileInstance.transform.rotation = getFixedProjectileRotation();
            projectileInstance.AddComponent<Projectile>().setData(target, getProjectileDuration(), getProjectileSpeed(), getFixedProjectileRotation()).tag = "projectile";
            projectileInstance.tag = "projectile";
            projectile.SetActive(false);
        }
    }

    protected void getTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float tmpDistance = Mathf.Infinity;
        GameObject tmpEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < tmpDistance)
            {
                tmpDistance = distanceToEnemy;
                tmpEnemy = enemy;
            }
        }
        if (tmpEnemy != null && tmpDistance <= range)
        {
            target = tmpEnemy;
        }
        else
        {
            target = null;
        }
    }

    private void rotate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;

            float turnSpeed = 2f;
            targetPosition.y = 0;

            // Fix forward vector
            Quaternion newRotation = Quaternion.LookRotation(transform.position - targetPosition, Vector3.forward) * getFixedRotation();
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
        }
    }
}
