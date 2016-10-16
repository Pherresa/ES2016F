using UnityEngine;
using System.Collections;

public class ShootingMove : MonoBehaviour
{

    public Vector3 postarget;

    // Use this for initialization
    void Start()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        //print(postarget);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = postarget - transform.position;
        transform.Translate(dir.normalized * 0.09f);
        if ((int)postarget.x == (int)transform.position.x && (int)postarget.y == (int)transform.position.y && (int)postarget.z == (int)transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     print("ddddd");
    //    if (col.gameObject.name == "Enemy")
    //     {
    //        Destroy(col.gameObject);
    //}
}