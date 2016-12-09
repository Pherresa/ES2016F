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
    public int damage;
    private int money;

    public AudioClip soundAttacked;
    public AudioClip soundDeath;
	public AudioClip soundSword;

    private AudioSource source {
        get{
            //MainCamera mc = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;
            return Camera.main.GetComponent<AudioSource> ();
            //return mc.GetComponent<AudioSource> ();

        }
    }


    private GameObject explosion;

    // Use this for initialization
    void Start () {

        soundAttacked = Resources.Load("SoundEffects/bomb") as AudioClip;

        soundDeath = Resources.Load("SoundEffects/enemyDead") as AudioClip;

		soundSword = Resources.Load("SoundEffects/soundSword") as AudioClip;

        explosion = Resources.Load("Prefabs/Explosion") as GameObject;
        life = 100f;
        maxlife = 100f;
        money = Enemy_Values_Gene.m_medium_enemy("m");
        //initText();
        m_moviments = new Queue();
        m_moviments.Enqueue(new Vector3(276, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(276, 60, 215)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 412)); // TEST
        m_movi_actu = (Vector3)m_moviments.Dequeue();
        this.gameObject.AddComponent<Collider>();
    }



    public void playSound(AudioClip audio){
        source.PlayOneShot (audio);
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
    }

    private void checkLife()
    {
        if(life <= 0)
        {
            playSound(soundDeath);
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject.Find("GameManager").GetComponent<GameManager>().GainAmount(money);
            GameObject.Find("GameManager").GetComponent<GameManager>().updateCurrentScore(5);
            Destroy(gameObject);
            playSound(soundDeath);

        }
    }

    void OnCollisionEnter(Collision col)
    {
		 
        if (col.gameObject.tag == "projectile")
        {
            //Destroy(this.gameObject);
            playSound(soundAttacked);
            //print(col.gameObject.name);
            life -= 50f;
        }
		/*
		if (col.gameObject.tag == "rohanHorse")
		{
			//Destroy(this.gameObject);
			playSound(soundAttacked);
			//print(col.gameObject.name);
			life -= 50f;
		}*/

    } 


}
