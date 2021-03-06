﻿using UnityEngine;
using System.Collections;
/*  Clase abstracta para definir los atributos que tiene la torrer y funciones que tiene que tener las
 *  unidades de defensa como minimo.
*/
public abstract class BaseEnemy : MonoBehaviour {
    
    public enum EnemyType
    {
        ENT,
        UNKNOWN
    }
    protected int walkStateHash = Animator.StringToHash("Base Layer.walk");
    protected int attackStateHash = Animator.StringToHash("Base Layer.attack");

    
    
    //FLAGS
    protected bool autoRotate = true;
    protected int shootStateHash = Animator.StringToHash("Shoot");

    //Internal vars
    protected bool active;
    protected int level;
    protected Animator animator;
    protected bool isShooting = false;
    protected int currentAttackCount = 0;

    //External vars
    public EnemyType type = EnemyType.UNKNOWN;
    public int strenght;

    //Common vars
    protected GameObject target;
    public GameObject projectile;

 
    protected abstract Quaternion getFixedProjectileRotation();
    protected abstract float getProjectileDuration();
    protected abstract float getProjectileSpeed();
    protected abstract int getNumAttacks();

    public void destroyEnemy() { Destroy(gameObject); }
    public bool isActiveEnemy() { return active; }
    public void activate() { active = true; }
    public void disable() { active = false; }
    public int getCurrentAnimationStateHash() { return animator != null ? animator.GetCurrentAnimatorStateInfo(0).fullPathHash : -1; }
    public int getNextAnimationStateHash() { return animator != null ? animator.GetNextAnimatorStateInfo(0).fullPathHash : -1; }

    public int getWalkStateHash() { return walkStateHash; }
    public int getAttackStateHash() { return attackStateHash; }

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
        //if (autoRotate) rotate();
    }

    public void shoot()
    {
        if (target != null && getCurrentAnimationStateHash() == getWalkStateHash())
        {
            if (currentAttackCount < getNumAttacks())
            {
                projectile.SetActive(true);
                startShootAnimation();
                isShooting = false;
            }
        } else if (target == null && getCurrentAnimationStateHash() == getWalkStateHash())
        {
            projectile.SetActive(true);
        }
    }

    private void checkAnimationState()
    {
        if (!isShooting && animator.IsInTransition(0) && getCurrentAnimationStateHash() == getAttackStateHash())
        {
            generateProjectile();
            isShooting = true;
            //currentAttackCount++;
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
            projectileInstance.transform.parent = transform;
           // projectileInstance.transform.localScale = transform.localScale;
            projectileInstance.transform.position = projectile.transform.position;// + new Vector3(0f, 3f, 0f);
            projectileInstance.transform.rotation = getFixedProjectileRotation();
            //projectileInstance.AddComponent<ShootingMove>().setData(target).tag = "projectile";
            projectileInstance.AddComponent<Projectile>().setData(target, getProjectileDuration(), getProjectileSpeed(), getFixedProjectileRotation()).tag = "projectile";
            projectileInstance.tag = "projectile";
            projectile.SetActive(false);
        }
    }

    protected void getTarget()
    {
        GameObject mTower = GameObject.FindGameObjectWithTag("MainTower");
        float distanceToMainTower = Vector3.Distance(transform.position, mTower.transform.position);
        if (distanceToMainTower <= 50f)
        {
            target = mTower;
        }
        else
        {
            target = null;
        }
    }

   /** private void rotate()
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
    }**/
}
