using UnityEngine;
using System.Collections;
using System;
/*
 * Clase funcional de la unidades de defensa. Esta clase dispone de las funciones de Tower como asi 
 * algunas aducionales que han hecho falta. Esta clase la utilizaran generalmente las clases que
 * disparen desde lejos.
 */
public class Action_Defense : Tower
{
    public int towerPrice;
    public int towerTama;
    Animation anim;

    //Animation animation;
    private float timer = 0.6f;
    private int predict;
    private Vector3 posIni;
    private int maxFrameToPredict = 8;
    private int plusToPredict = 6;
    // Funcion constructora por defecto. Inicializa variables.Aqui se debera leer de la BBDD i asignar
    // su valor a los respectivos atributos.
    void Start()
    {
        iniStates();
    }

	// Funcion que se executa por cada frame para poder girar correctamente the towers.
	void Update() {
		// vemos que no sea nulo
		if (target != null) {
			// llamamos la funcion que gira al towers
			SpinTower.spin(target.transform.position, this.transform);
		}
	}

    // funcion que se ejecuta continuamente.
    void FixedUpdate()
    {
        if (active)
        {
            if (predict == 0)
            {

                getTarget();
                if (target != null)
                {
                    posIni = target.transform.position;
                    predict += 1;
                }
            }
            else
            {
                if (predict != maxFrameToPredict)
                {
                    predictPositionToShoot();
                }

            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                timer = 0.6f;
                
                if (type != 0 && predict == maxFrameToPredict)
                {
                    if (!anim.isPlaying)
                    {
                        anim.Play("A_Trebuchet_attack");
                        shoot();
                        predict = 0;
                    }else
                    {
                        anim.Play("A_Trebuchet_recharge");
                    }
                    
                }
                // }
            }
        }
    }
        
        

    // inicializador
    void iniStates()
    {
        get_value_tower();
        //range = 40f;
        //strenght = 1;
        //predict = 0;
        getTypeOfDefense();
    }
    // para definir el tipo de defensa que es (prefab) buscandolo por el nombre
    private void getTypeOfDefense()
    {
        String name = this.gameObject.name.Split('(')[0];
        if (name == "defense1_Trebuchet_MT")
        {
            type = 1;
            anim = gameObject.GetComponent<Animation>();
            anim.Play("A_Trebuchet_idle");
            
        }
        if (name == "defense2_RohanBarracks_MT")
        {
            type = 2;
        }
        if (name == "defense2_OrcArcher_I")
        {
            type = 3;
        }
        if (name == "defense3_MercenaryHuman_I")
        {
            type = 4;
        }
    }
    // Para destruir la torre
    protected override void destroyTower()
    {
        Destroy(this.gameObject);
    }
    // simular disparo
    protected override void shoot()
    {
        if (target != null)
        {
            
            float distanceToEnemy = Vector3.Distance(this.transform.position, posIni);
            if (distanceToEnemy <= range)
            { 
                shootProjectile();
            }
        }else
        {
            predict = 0;
        }
    }
    // 'apunta' al enemigo que va a atacar. Obtiene una posicion/coordenadas avanzada
    private void predictPositionToShoot()
    {
        Vector3 tmp;
        if (target != null)
        {
            if (predict == maxFrameToPredict - 1)
            {
                predict = maxFrameToPredict;
                tmp = (target.transform.position - posIni);
                float distanceToEnemy = Vector3.Distance(target.transform.position, posIni);
                posIni = target.transform.position + (tmp * plusToPredict);

                if (distanceToEnemy < range && distanceToEnemy > range/2.0f)
                {
                    posIni *= 4;
                }
                else if (distanceToEnemy < range && distanceToEnemy > range / 3.0f)
                {
                    posIni *= 3;
                }
                else if (distanceToEnemy < range/3.0f && distanceToEnemy > range/4.0f)
                {
                    posIni *= 1.5f;
                }
                
            }
            if (predict != maxFrameToPredict)
            {
                predict += 1;
            }
        }else
        {
            predict = 0;
        }
    }
    // crea el proyectil. Como muchos no estan creados todavia, se genera ua sphera
    private GameObject shootProjectile()
    {
        GameObject pro = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pro.AddComponent<Rigidbody>();
        pro.AddComponent<Collider>();
        Vector3 tmp = this.transform.position;
        tmp.y += 2;
        pro.transform.position = tmp;
        pro.transform.localScale = new Vector3(2f, 2f, 2f);
        pro.AddComponent<ShootingMove>();
        pro.GetComponent<ShootingMove>().pos = posIni;
        pro.GetComponent<ShootingMove>().tag = "projectile";
        if (type == 1)
        { 
            pro.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (type == 2)
        {
            pro.GetComponent<Renderer>().material.color = Color.green;
        }
        if (type == 3)
        {
            pro.GetComponent<Renderer>().material.color = Color.red;
        }
        if (type == 4)
        {
            pro.GetComponent<Renderer>().material.color = Color.yellow;
        }
        return projectile;
    }
    // obtiene el enemigo mas cercano
    protected override void getTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
        }
         else
         {
             target = null;
             predict = 0;
         }
    }
    // para saber si esta activa o no.
    public override bool isActiveTower()
    {
        return active;
    }
    // activar la torre para que dispare
    public override void activate()
    {
        active = true;
    }
    // desactivar la torre para que no dispare
    public override void disable()
    {
        active = false;
    }

    private void get_value_tower()
    {
        switch (towerTama)
        {
            case 1:
                range = Enemy_Values_Gene.m_little_tower("r");
                strenght = Enemy_Values_Gene.m_little_tower("a");
                towerPrice = (int) Enemy_Values_Gene.m_little_tower("m") / 2;
                break;
            case 2:
                range = Enemy_Values_Gene.m_medium_tower("r");
                strenght = Enemy_Values_Gene.m_medium_tower("a");
                towerPrice = (int)Enemy_Values_Gene.m_medium_tower("m") / 2;
                break;
            case 3:
                range = Enemy_Values_Gene.m_big_tower("r");
                strenght = Enemy_Values_Gene.m_big_tower("a");
                towerPrice = (int)Enemy_Values_Gene.m_big_tower("m") / 2;
                break;
            default:
                Debug.Log("This type does not exist.");
                break;
        }
        predict = 0;
    }
}

