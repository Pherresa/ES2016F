using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int m_velocity;
    private Queue m_moviments;
    private Vector3 m_movi_actu;

    // Use this for initialization
    void Start () {
           
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position_aprox = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);
        if (position_aprox == m_movi_actu)
        {
            m_movi_actu = (Vector3)m_moviments.Dequeue();
        }
        transform.LookAt(m_movi_actu);
        this.transform.Translate(Vector3.forward * m_velocity * Time.deltaTime);
    }
}
