using UnityEngine;
using System.Collections;
using System;

public class Elf : MonoBehaviour {

    Animation anim;
    AnimationState stateElfMoving;
    AnimationState stateElfAttacking;

    GameObject target;
    //public Vector3 center;
    //public int range = 60;

    DateTime timeOnPlay;
    private Vector3 newPos;
    private Vector3 newForward;
    private Transform model;

    // Use this for initialization
    void Start()
    {

        //anim = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        anim = this.gameObject.GetComponentInChildren<Transform>().Find("body").GetComponent<Animation>();
        
        InitAnimation();
        newPos = transform.position;

        target = GameObject.FindGameObjectWithTag("Target");
    }


    void InitAnimation()
    {

        anim["Caminar_elfo"].speed = 1f;
        stateElfMoving = anim["Caminar_elfo"];
        stateElfMoving.time = 0;
        stateElfMoving.enabled = true;
        anim.Sample();
        stateElfMoving.enabled = false;
        
        anim["Ataque_Elfo"].speed = 0.5f;
        stateElfAttacking = anim["Ataque_Elfo"];
        stateElfAttacking.time = 0;
        stateElfAttacking.enabled = true;
        anim.Sample();
        stateElfAttacking.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.transform.position - this.transform.position;

        if (dir.magnitude < 50)
        {
            this.gameObject.GetComponent<AstarAI3>().enabled = false;
            gameObject.transform.LookAt(target.transform.position);
            anim.Play("Ataque_Elfo");
            
        }
        else
        {
            anim.Play("Caminar_elfo");
        }
        /*
        if (Vector3.Distance(newPos, transform.position) > 1)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);

        }
        
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
        */
    }

}