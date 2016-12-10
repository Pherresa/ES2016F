using UnityEngine;
using System.Collections;
using System;

public class Elephant : MonoBehaviour
{


    Animation anim;
    AnimationState stateElephantMoving;
    AnimationState stateElephantAttacking;

    GameObject target;
    public Vector3 center;
    public int range = 60;

    DateTime timeOnPlay;
    private Vector3 newPos;
    private Vector3 newForward;
    private Transform model;

    // Use this for initialization
    void Start()
    {
        
        anim = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        InitAnimation();
        newPos = transform.position;
       

    }

  
    void InitAnimation()
    {

        anim["A_Oliphant_moving"].speed = 2f;
        stateElephantMoving = anim["A_Oliphant_moving"];
        stateElephantMoving.time = 0;
        stateElephantMoving.enabled = true;
        anim.Sample();
        stateElephantMoving.enabled = false;

        anim["A_Oliphant_attack"].speed = 2f;
        stateElephantAttacking = anim["A_Oliphant_attack"];
        stateElephantAttacking.time = 0;
        stateElephantAttacking.enabled = true;
        anim.Sample();
        stateElephantAttacking.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(newPos, transform.position) > 1)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
          
        }
        target = GameObject.FindGameObjectWithTag("Target");
        if (target != null)
        {
            Vector3 dir = target.transform.position - this.transform.position;
          
            if (dir.magnitude < 10)
            {
                Debug.Log("Attacking");
                anim.Play("A_Oliphant_attack");
                //animationPhase = 0;
            }
            else
            {
              
                anim.Play("A_Oliphant_moving");
            }

        }
        else
        {
          
            anim.Play("A_Oliphant_moving");

        }

    }

}