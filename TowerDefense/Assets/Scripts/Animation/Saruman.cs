using UnityEngine;
using System.Collections;
using System;

public class Saruman : MonoBehaviour {

    GameObject target;
    GameObject pro;
    GameObject pro_i;

    Animation anim;
    AnimationState stateSarumanAttacking;

    DateTime timeOnPlay;
    bool charge;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        pro = (GameObject)Resources.Load("Prefabs/attack3P_Elf_II");//defense1P_saruman

        anim["ArmatureAction_001"].speed = 1f;
        stateSarumanAttacking = anim["ArmatureAction_001"];
        stateSarumanAttacking.time = 0;
        stateSarumanAttacking.enabled = true;
        anim.Sample();
        stateSarumanAttacking.enabled = false;
        charge = true;
    }
	
	// Update is called once per frame
	void Update () {
        getTarget();
        if (target!=null) {
            if (charge) {
                timeOnPlay = DateTime.Now;
                anim.Play("ArmatureAction_001");
                charge = false;
            }
            if ((DateTime.Now - timeOnPlay).Seconds > 0.7f)//anim["ArmatureAction_001"].length)
            {
                // Disparar
                //anim.Play("ArmatureAction_001");
                //anim.Stop("ArmatureAction_001");
                getTarget();
                pro_i = Instantiate(pro);
                pro_i.transform.position = this.transform.GetChild(this.transform.childCount - 2).transform.position;
                pro_i.AddComponent<ShootingBall>();
                pro_i.GetComponent<ShootingBall>().setTarget(target);
                pro_i.GetComponent<ShootingBall>().tag = "projectile";
                charge = true;
            }
        }
    }

    void FixedUpdate() {
        //getTarget();
    }

    private void getTarget()
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
        if (tmpEnemy != null && tmpDistance <= GetComponent<Action_Defense>().getValues().range)
        {
            target = tmpEnemy;
        }
        else
        {
            target = null;
        }
    }
}
