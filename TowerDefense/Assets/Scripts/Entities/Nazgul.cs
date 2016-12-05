using UnityEngine;
using System.Collections;

public class Nazgul : MonoBehaviour {

    private Queue m_moviments;
    private Vector3 m_movi_actu;
    private int m_velocity;
    private Start_Round s_r;
    private bool final = false;

    private Animation anima;
    private AnimationState anima_st;


    // Use this for initialization
    void Start () { // position Start -37 75 -81
        m_velocity = 10;
        m_moviments = new Queue();
        m_moviments.Enqueue(new Vector3(-42, 75, -42)); // TEST
        m_moviments.Enqueue(new Vector3(41, 75, 16)); // TEST
        m_moviments.Enqueue(new Vector3(56, 75, 40)); // TEST
        m_moviments.Enqueue(new Vector3(36, 75, 55)); // TEST
        m_moviments.Enqueue(new Vector3(2, 75, 68)); // TEST
        m_moviments.Enqueue(new Vector3(-24, 75, 53)); // TEST
        m_moviments.Enqueue(new Vector3(-45, 75, 35)); // TEST
        m_moviments.Enqueue(new Vector3(49, 75, -40)); // TEST
        m_moviments.Enqueue(new Vector3(60, 75, -60)); // TEST
        m_moviments.Enqueue(new Vector3(37, 75, -71)); // TEST
        m_moviments.Enqueue(new Vector3(5, 75, -82)); // TEST
        m_moviments.Enqueue(new Vector3(-25, 75, -73)); // TEST
        m_moviments.Enqueue(new Vector3(-37, 75, -81)); // TEST
        m_movi_actu = (Vector3)m_moviments.Dequeue();
        s_r = GameObject.Find("Play").GetComponent<Start_Round>();
        anima = this.GetComponent<Animation>();
        anima["A_Nazgul_moving"].speed = 0.5f;
        anima_st = anima["A_Nazgul_moving"];
        anima_st.time = 0;
        anima_st.enabled = true;
        anima.Sample();
        anima_st.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position_aprox = new Vector3((int)Mathf.Round(this.transform.position.x), (int)Mathf.Round(this.transform.position.y), (int)Mathf.Round(this.transform.position.z)); // We round the value, otherwise in certain cases may not work
        if (position_aprox == m_movi_actu)
        {
            if (!final)
            {
                m_moviments.Enqueue(m_movi_actu);
                m_movi_actu = (Vector3)m_moviments.Dequeue();
            }
        }
        transform.LookAt(m_movi_actu);
        this.transform.Translate(Vector3.forward * m_velocity * Time.deltaTime);
        anima.Play("A_Nazgul_moving");
    }
}
