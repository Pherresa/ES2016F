using UnityEngine;
using System.Collections;
using System;

public class Elf : MonoBehaviour {

    Animation anim;
    AnimationState stateElfMoving;
    AnimationState stateElfAttacking;

    GameObject target;
    bool charge;
    bool finish;
    DateTime timeOnPlay;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponentInChildren<Transform>().Find("body").GetComponent<Animation>();
        InitAnimation();
        
        charge = true;
        finish = false;
        target = GameObject.FindGameObjectWithTag("Target");
    }


    void InitAnimation()
    {

        anim["Caminar_elfo"].speed = 0.7f;
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

        if (dir.magnitude < 45 && !finish)
        {
            if (charge)
            {
                this.gameObject.tag = "Untagged";
                this.gameObject.GetComponent<AstarAI3>().enabled = false;
                timeOnPlay = DateTime.Now;
                anim.Play("Ataque_Elfo");
                charge = false;
            }
            else {
                gameObject.transform.Rotate(new Vector3(14.75f, 183.456f, -19.402f));
                if ((DateTime.Now - timeOnPlay).Seconds > 1f) {
                    GameObject proj = (GameObject)Resources.Load("Prefabs/attack3P_Elf_I");
                    proj = Instantiate(proj);
                    proj.AddComponent<Rigidbody>();
                    proj.transform.position= this.gameObject.GetComponentInChildren<Transform>().Find("body").GetComponentInChildren<Transform>().Find("Flecha").transform.position;
                    proj.AddComponent<ShootingMove>();
                    proj.GetComponent<ShootingMove>().pos = target.transform.position;
                    proj.GetComponent<ShootingMove>().tag = "projectile";
                    finish = true;
                    this.gameObject.GetComponent<AstarAI3>().enabled = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().LoseLife(this.gameObject.GetComponent<Enemy>().getValues().damage);
                }
            }
        }
        else
        {
            anim.Play("Caminar_elfo");
        }
    }

}