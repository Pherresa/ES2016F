using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour {

    public enum EnemyType {
        UNKNOWN,
        ENT,
        ORC_MT,
        OLIPHANT_MT,
        ELF_I,
        BATTERINGRAM_MT,
        HOBBIT_I,
        NAZGUL,
        ENEMY
    }

    public struct Value {
        public float maxlife;
        public int damage;
        public int money;
        public int speed;
        public EnemyType type;
    }

    public int m_velocity;
    private Queue m_moviments;
    private Vector3 m_movi_actu;

    private GameObject textObject;
    private Text textLife;
    private RectTransform trans;

    public float life;
    Value enem;

    private Enemy_Values_Gene gener_va;

    public AudioClip soundAttacked;
    public AudioClip soundDeath;
	public AudioClip soundSword;
	DateTime timeOnPlay;
	Vector3 pos_previous; 
	Vector3 pos_init; 

    private AudioSource source {
        get{
            //MainCamera mc = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;
            return Camera.main.GetComponent<AudioSource> ();
            //return mc.GetComponent<AudioSource> ();

        }
    }


    private GameObject explosion;

    void Awake() {
        gener_va = new Enemy_Values_Gene();
        getTypeOfEnemy();
		gener_va.asig_values_enemy(ref enem);
		pos_previous = this.transform.position;
		pos_init = this.transform.position;
    }

    // Use this for initialization
    void Start () {
        soundAttacked = Resources.Load("SoundEffects/bomb") as AudioClip;

        soundDeath = Resources.Load("SoundEffects/enemyDead") as AudioClip;

		soundSword = Resources.Load("SoundEffects/soundSword") as AudioClip;

        explosion = Resources.Load("Prefabs/Explosion") as GameObject;

        life = enem.maxlife;
        //enem.maxlife = 100f;
        //enem.money = 0;// Enemy_Values_Gene.m_medium_enemy("m");
        //initText();
        m_moviments = new Queue();
        m_moviments.Enqueue(new Vector3(276, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(276, 60, 215)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 389)); // TEST
        m_moviments.Enqueue(new Vector3(155, 60, 412)); // TEST
        m_movi_actu = (Vector3)m_moviments.Dequeue();
        this.gameObject.AddComponent<Collider>();

		timeOnPlay = DateTime.Now;
    }



    public void playSound(AudioClip audio){
        source.PlayOneShot (audio);
    }


    void initText(){
        print("initText");
        textObject = new GameObject();
        textObject.transform.SetParent(GameObject.Find("Canvas").transform);


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 w = new Vector3(pos.x, pos.y, 0.0f);
        
        //Vector3 positionEx = new Vector3(0.0f, 160.0f, 0.0f);
        
        trans = textObject.AddComponent<RectTransform>();
        //trans.anchoredPosition = new Vector2(10.0f, 10.0f);
        trans.position = pos;


        textLife = textObject.AddComponent<Text>();


        Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
        textLife.font = ArialFont;
        textLife.name = "Life";
        textLife.text = life + "/" + enem.maxlife;
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
        //Vector3 w = new Vector3(pos.x, pos.y, 0.0f);
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

		checkEnemyPosition ();

        checkLife();
    }

    private void checkLife()
    {
        if(life <= 0)
        {
            playSound(soundDeath);
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject.Find("GameManager").GetComponent<GameManager>().GainAmount(enem.money);
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
        if (col.gameObject.tag == "projectile_Saru")
        {
            
            playSound(soundAttacked);
            //Debug.Log(col.gameObject.GetComponent<ShootingBall>().getDamage());
            life -= col.gameObject.GetComponent<ShootingBall>().getDamage();
            Destroy(col.gameObject);
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

    private void getTypeOfEnemy()
    {
        String name = this.gameObject.name.Split('(')[0];
        if (name == "attack4_BatteringRam_MT")
        {
            enem.type = EnemyType.BATTERINGRAM_MT;
        }
        else if (name == "attack3_Elf_I")
        {
            enem.type = EnemyType.ELF_I;
        }
        else if (name == "attack1_Ent_I")
        {
            enem.type = EnemyType.ENT;
        }
        else if (name == "attack4_Hobbit_I")
        {
            enem.type = EnemyType.HOBBIT_I;
        }
        else if (name == "attack2_Oliphant_MT")
        {
            enem.type = EnemyType.OLIPHANT_MT;
        }
        else if (name == "attack1_Orc_MT")
        {
            enem.type = EnemyType.ORC_MT;
        }
        else if (name == "attack3_NazgulV_MT")
        {
            enem.type = EnemyType.NAZGUL;
        }
        else if (name == "Enemy")
        {
            enem.type = EnemyType.ENEMY;
        }
        else
        {
            enem.type = EnemyType.UNKNOWN;
        }
    }

    public Value getValues() {
        return enem;
    }

	private void checkEnemyPosition()
	{ 

		if ((DateTime.Now - timeOnPlay).Seconds > 1.5f) {

			if (Vector3.Distance (pos_previous, this.transform.position) < 0.1 && Vector3.Distance(pos_init,pos_previous) > 5f) {

				playSound(soundDeath);
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy (this.gameObject);
			} else {
				pos_previous = this.transform.position;
			}

		}

	}
}
