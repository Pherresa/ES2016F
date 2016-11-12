using UnityEngine;
using System.Collections;

public class ShootingMove : MonoBehaviour
{

    public GameObject target;
    public float speed = 4f;
    public float firingAngle = 30.0f;
    public float gravity = 9.8f;
    public Transform Projectile;
    private Transform myTransform;
    Vector3 pos;

    void Start()
    {
        pos = target.transform.position;
    }

    void FixedUpdate()
    {
        Projectile = transform;
        myTransform = transform;
        Projectile.position = myTransform.position + new Vector3(0, 0.15f, 0);
        float target_Distance = Vector3.Distance(Projectile.position, pos);
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        float Vx = Mathf.Sqrt(projectile_Velocity * speed) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity * speed) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        Projectile.rotation = Quaternion.LookRotation(pos - Projectile.position);
        float elapse_time = 0;
        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
        elapse_time += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Enemy") || col.gameObject.name == ("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }
}
