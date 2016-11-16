using UnityEngine;
using System.Collections;

/*
 * Clase que va a utilizar el proyectil
 */ 
public class ShootingMove : MonoBehaviour
{

    //public GameObject target;
    private float speed = 70f;
    private float firingAngle = 40.0f;
    private float gravity = 9.8f;
    private Transform Projectile;
    private Transform myTransform;
    public Vector3 pos;

    void Start()
    {
        //pos = target.transform.position;
    }

    void FixedUpdate()
    {
        // La siguiente linia es para hacer pruebas. Si se descomenta los proyectiles obtienen la posicion
        // del bicho al que atacan y lo perseguira por la scena hasta darle. 
        // Si la dejas comentada (como deberia ser), el proyectil va a una posicion. Esta opcion es mas realista.
        //pos = target.transform.position;
        
        Projectile = transform;
        myTransform = transform;
        Projectile.position = myTransform.position;
        //pos.x += 2;
        Projectile.LookAt(pos);
        float target_Distance = Vector3.Distance(Projectile.position, pos);
        float projectile_Velocity = target_Distance / (Mathf.Sin(2f* firingAngle * Mathf.Deg2Rad) / gravity);
        float Vx = Mathf.Sqrt(projectile_Velocity * speed) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity * speed) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        Projectile.rotation = Quaternion.LookRotation(pos - Projectile.position);
        float elapse_time = 0;
        Projectile.Translate(0, ((Vy) - (gravity)) * Time.deltaTime, (Vx) * Time.deltaTime);
        elapse_time += Time.deltaTime;
    }
    // cuando colisione con un objeto de los que indico se destruya.
    void OnCollisionEnter(Collision col)
    {
        
            Destroy(this.gameObject);
        
    }
}
