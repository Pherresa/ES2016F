using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int m_velocity;
    private Queue m_moviments;
    private Vector3 m_movi_actu;

    // Use this for initialization
    void Start () {
        m_moviments = new Queue();
        m_moviments.Enqueue(new Vector3(276, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(276, 60, 215)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 412)); // TEST
        m_movi_actu = (Vector3)m_moviments.Dequeue();
    }

    // Update is called once per frame
    // For every frame moves the character his speed to get the movement per second instead of per frame multiply 
    // the speed DeltaTime, face the enemy and tell a unit to move forward if the initial position is equal to the 
    // end we get another point the FIFO queue.
    void Update () {
        Vector3 position_aprox = new Vector3((int)Mathf.Round(this.transform.position.x), (int)Mathf.Round(this.transform.position.y), (int)Mathf.Round(this.transform.position.z)); // We round the value, otherwise in certain cases may not work
        if (position_aprox == m_movi_actu)
        {
            m_movi_actu = (Vector3)m_moviments.Dequeue();
        }
        transform.LookAt(m_movi_actu);
        this.transform.Translate(Vector3.forward * m_velocity * Time.deltaTime);
    }
}
