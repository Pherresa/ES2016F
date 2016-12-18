using UnityEngine;
using System.Collections;

public class ShootingBall : MonoBehaviour
{

    Vector3 objec;
    int damage;

    float velo;

    // Use this for initialization
    void Start()
    {
        velo = 100;
        //damage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (objec == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.transform.LookAt(objec);
            //this.gameObject.transform.LookAt(objec.GetComponent<CharacterController>().transform.position);
            this.transform.Translate(Vector3.forward * velo * Time.deltaTime);
        }
    }

    public void setTarget(Vector3 t)
    {
        objec = t;
    }

    public void setDamage(int d)
    {
        damage = d;
    }

    public void setVel(float v)
    {
        velo = v;
    }

    public int getDamage()
    {
        return damage;
    }

    void OnCollisionEnter(Collision col)
    {
    }
}