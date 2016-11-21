using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour {

    public int m_velocity;
    private Queue m_moviments;
    private Vector3 m_movi_actu;

    private GameObject textObject;
    private Text textLife;
    private RectTransform trans;

    public float life;
    public float maxlife;

    private GameObject explosion;
    private float lifeVirus;

    protected GameObject target;
    protected float range;
    private Vector3 posIni;

    // Use this for initialization
    void Start () {
        explosion = Resources.Load("Prefabs/Explosion") as GameObject;
        lifeVirus = UnityEngine.Random.Range(0.5f, 10f);
        life = 100f;
        maxlife = 100f;
        range = 40f;
        //initText();
    }

    void initText(){
        print("initText");
        textObject = new GameObject();
        textObject.transform.SetParent(GameObject.Find("Canvas").transform);


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 w = new Vector3(pos.x, pos.y, 0.0f);
        
        Vector3 positionEx = new Vector3(0.0f, 160.0f, 0.0f);
        
        trans = textObject.AddComponent<RectTransform>();
        //trans.anchoredPosition = new Vector2(10.0f, 10.0f);
        trans.position = pos;


        textLife = textObject.AddComponent<Text>();


        Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
        textLife.font = ArialFont;
        textLife.name = "Life";
        textLife.text = life + "/" + maxlife;
        textLife.fontSize = 10;
        textLife.color = Color.red;
 
        textLife.transform.position = pos;

    }

    // Update is called once per frame
    // For every frame moves the character his speed to get the movement per second instead of per frame multiply 
    // the speed DeltaTime, face the enemy and tell a unit to move forward if the initial position is equal to the 
    // end we get another point the FIFO queue.
    void Update () {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 w = new Vector3(pos.x, pos.y, 0.0f);
        //textLife.transform.position = pos;
        //trans.position = pos;

        Vector3 position_aprox = new Vector3((int)Mathf.Round(this.transform.position.x), (int)Mathf.Round(this.transform.position.y), (int)Mathf.Round(this.transform.position.z)); // We round the value, otherwise in certain cases may not work
        if (position_aprox == m_movi_actu)
        {
            m_movi_actu = (Vector3)m_moviments.Dequeue();
            //textLife.text ="";
        }
        transform.LookAt(m_movi_actu);
        this.transform.Translate(Vector3.forward * m_velocity * Time.deltaTime);

        checkLife();
        getTarget();
        
    }

    protected void checkLife()
    {
        if(life <= 0)
        {
            Destroy(this);
        }
    }

    protected void getTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");
        float tmpDistance = Mathf.Infinity;
        GameObject tmpEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < tmpDistance)
            {
                tmpDistance = distanceToEnemy;
                tmpEnemy = enemy;
            }
        }
        if (tmpEnemy != null && tmpDistance <= range)
        {
            target = tmpEnemy;
            shoot();
        }
        else
        {
            target = null;
        }
    }

    protected void shoot()
    {
        GameObject pro = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pro.AddComponent<Rigidbody>();
        pro.AddComponent<Collider>();
        Vector3 tmp = this.transform.position;
        tmp.y += 2;
        pro.transform.position = tmp;
        pro.transform.localScale = new Vector3(2f, 2f, 2f);
        posIni = target.transform.position;
        pro.AddComponent<ShootingMove>();
        pro.GetComponent<ShootingMove>().pos = posIni;
        pro.GetComponent<ShootingMove>().tag = "projectile";
        pro.GetComponent<Renderer>().material.color = Color.blue;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "projectile")
        {
            life -= 50;
            checkLife();
        }
    }
}
