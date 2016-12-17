using UnityEngine;
using System.Collections;
using System;

public class Nazgul : MonoBehaviour {

    private Queue m_moviments;
    private Vector3 m_movi_actu;
    private int m_velocity;
    private Start_Round s_r;
    private bool final = false;
    private bool final2 = false;
    private bool charge = false;
    private bool fire = false;
    GameObject enemy;
    DateTime timeOnPlay;

    private Animation anima;
    private AnimationState anima_st;
    private AnimationState anima_at;


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
        
        if (s_r.actu_round() >= s_r.total_round)
        {
            m_movi_actu = GameObject.Find("StartCube").transform.position;
            final = true;
        }
        if (position_aprox == m_movi_actu || (m_movi_actu-position_aprox).magnitude < 10)
        {
            if (!final)
            {
                m_moviments.Enqueue(m_movi_actu);
                m_movi_actu = (Vector3)m_moviments.Dequeue();
            }
            else{
                //this.gameObject.SetActive(false);
                this.gameObject.transform.position = new Vector3(0,-50,0);
                //Debug.Log("asd");
                GameObject enemyPrefab = (GameObject) Resources.Load("Prefabs/attack3_NazgulV_MT");
                enemy = Instantiate(enemyPrefab);
                enemy.transform.parent = GameObject.Find("EnemyManager").transform;
                //get the thing component on your instantiated object
                Vector3 p_ini=GameObject.Find("StartCube").transform.position;
                enemy.transform.position = new Vector3(p_ini.x, p_ini.y, p_ini.z);
                AstarAI astarAI = enemy.GetComponent<AstarAI>();
                astarAI.speed = enemy.GetComponent<Enemy>().getValues().speed;
                astarAI.target = GameObject.FindGameObjectWithTag("Target").transform;
                anima= enemy.GetComponentInChildren<Transform>().Find("body").GetComponent<Animation>();
                //anima = this.GetComponent<Animation>();
                anima["A_Nazgul_moving"].speed = 0.5f;
                anima_st = anima["A_Nazgul_moving"];
                anima_st.time = 0;
                anima_st.enabled = true;
                anima.Sample();
                anima_st.enabled = false;
                final2 = true;

                anima["A_Nazgul_attack"].speed = 0.8f;
                anima_at = anima["A_Nazgul_attack"];
                anima_at.time = 0;
                anima_at.enabled = true;
                anima.Sample();
                anima_at.enabled = false;
            }
        }
        if (!final2)
        {
            this.transform.LookAt(m_movi_actu);
            this.transform.Translate(Vector3.forward * m_velocity * Time.deltaTime);
            anima.Play("A_Nazgul_moving");
        }
        else {
            if (enemy != null)
            {
                Vector3 dir = GameObject.FindGameObjectWithTag("Target").transform.transform.position - enemy.transform.position;
                if (dir.magnitude < 34 && !fire)
                {
                    if (!charge)
                    {
                        //Debug.Log("ataque");
                        enemy.tag = "Untagged";
                        enemy.GetComponent<AstarAI>().enabled = false;
                        timeOnPlay = DateTime.Now;
                        anima.Play("A_Nazgul_attack");
                        charge = true;
                    }
                    else
                    {
                        enemy.transform.LookAt(GameObject.FindGameObjectWithTag("Target").transform.transform.position);
                        if ((DateTime.Now - timeOnPlay).Seconds > 0.9f)
                        {
                            Debug.Log("Fire");
                            //enemy.GetComponentInChildren<Transform>().Find("body").GetComponentInChildren<Transform>().Find("Sphere").GetComponent<MeshRenderer>().enabled = true;
                            GameObject proj = (GameObject)Resources.Load("Prefabs/attack3P_NazgulFire_MT");

                            proj = Instantiate(proj);
                            proj.transform.position = enemy.GetComponentInChildren<Transform>().Find("body").GetComponentInChildren<Transform>().Find("Sphere").transform.position;
                            proj.AddComponent<ShootingBall>();
                            proj.GetComponent<ShootingBall>().setTarget(new Vector3(-65.89f, 7.22f, 79.08f));
                            proj.GetComponent<ShootingBall>().setVel(1);
                        }
                        if ((DateTime.Now - timeOnPlay).Seconds > 1f)
                        {
                            //Debug.Log("Disparo");


                           
                            //proj.GetComponent<ShootingMove>().tag = "projectile";

                            enemy.GetComponent<AstarAI>().enabled = true;
                            fire = true;
                            GameObject.Find("GameManager").GetComponent<GameManager>().LoseLife(enemy.GetComponent<Enemy>().getValues().damage);
                        }
                    }

                }
                else
                {
                    anima.Play("A_Nazgul_moving");
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
