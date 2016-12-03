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
    AnimationState stateTrebuchetIdle;
    AnimationState stateTrebuchetAttack;
    AnimationState stateTrebuchetrecharge;
    private float timer = 1.5f;
    private int predict;
    private Vector3 posIni;
    private int maxFrameToPredict = 5;
    private int plusToPredictTrebu = 10;
    private int plusToPredict = 23;
    private int animationPhase = 0;
    private bool nextPhaseAnim = false;
    private float speed = 2f;
    private bool isShooting = false;
    int number = 0;
    bool couroutineStarted = false;
    int s = 3;
    DateTime timeOnPlay;
    // Funcion constructora por defecto. Inicializa variables.Aqui se debera leer de la BBDD i asignar
    // su valor a los respectivos atributos.
    void Start()
    {
        
        iniStates();
    }

    // Funcion que se executa por cada frame para poder girar correctamente the towers.
    void Update()
    {
        if (active)
        {
            checkPhaseAnim();
            // vemos que no sea nulo
            if (target != null)
            {
                // llamamos la funcion que gira al towers
                SpinTower.spin(target.transform.position, this.transform);
            }
        }
    }
    private void initAnimTrebuchet()
    {
        if (type==1) {
            anim["A_Trebuchet_idle"].speed = 1f;
            stateTrebuchetIdle = anim["A_Trebuchet_idle"];
            stateTrebuchetIdle.time = 0;
            stateTrebuchetIdle.enabled = true;
            anim.Sample();
            stateTrebuchetIdle.enabled = false;

            anim["A_Trebuchet_attack"].speed = 2.5f;
            stateTrebuchetAttack = anim["A_Trebuchet_attack"];
            stateTrebuchetAttack.time = 0;
            stateTrebuchetAttack.enabled = true;
            anim.Sample();
            stateTrebuchetAttack.enabled = false;

            anim["A_Trebuchet_recharge"].speed = 1.5f;
            stateTrebuchetrecharge = anim["A_Trebuchet_recharge"];
            stateTrebuchetrecharge.time = 0;
            stateTrebuchetrecharge.enabled = true;
            anim.Sample();
            stateTrebuchetrecharge.enabled = false;
        }
    }

    private void checkPhaseAnim()
    {
        if (isShooting) {
            if (type == 1)
            {
                if (animationPhase == 1)
                {
                    timeOnPlay = DateTime.Now;
                    anim.Play("A_Trebuchet_attack");
                    animationPhase = 2;
                }
                else if (animationPhase == 2)
                {
                    if ((DateTime.Now - timeOnPlay).Seconds > 0.4f)
                    {
                        lanzar();
                        timeOnPlay = DateTime.Now;
                        anim.Play("A_Trebuchet_recharge");
                        animationPhase = 3;
                    }
                }
                else if (animationPhase == 3)
                {
                    if ((DateTime.Now - timeOnPlay).Seconds > 1f)
                    {
                        isShooting = false;
                        animationPhase = 0;
                        predict = 0;
                    }
                }
            }
        }
        else
        {
            if (type == 1 && !anim.IsPlaying("A_Trebuchet_idle"))
            {
                anim.Play("A_Trebuchet_idle");
            }
        }
    }

    private void checkDistanceTarget()
    {
        float distanceToEnemy = Vector3.Distance(this.transform.position, posIni);
        if (distanceToEnemy > range)
        {
            target = null;
            animationPhase = 0;
            predict = 0;
            isShooting = false;
        }
    }
    // funcion que se ejecuta continuamente.
    void FixedUpdate()
    {
        if (active)
        {
            if (!isShooting) {
                if (animationPhase == 0)
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
                }
            }
            // mientras no este puesto las animaciones de las demas torres fuerzo a que pasen por aqui para que disparen
            if (type != 1 && predict == maxFrameToPredict)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 1.5f;
                    checkDistanceTarget();
                    if (target != null)
                    {
                        lanzar();
                        predict = 0;
                    }
                    else
                    {
                        predict = 0;
                    }
                }
            }
            // aqui solo entrara la catapulta por ahora
            else
            {
                if (!isShooting)
                {
                    {
                        checkDistanceTarget();
                        if (target != null)
                        {
                            if (type != 0 && predict == maxFrameToPredict)
                            {
                                shoot();

                            }
                        }
                    }
                }
            }
        }
    }



    // inicializador
    void iniStates()
    {
        get_value_tower();
        getTypeOfDefense();
        anim = GetComponent<Animation>();
        initAnimTrebuchet();
    }
    // para definir el tipo de defensa que es (prefab) buscandolo por el nombre
    private void getTypeOfDefense()
    {
        String name = this.gameObject.name.Split('(')[0];
        if (name == "defense1_Trebuchet_MT")
        {
            type = 1;
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
        isShooting = true;
        animationPhase = 1;
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
                if (type != 1)
                {
                    posIni = target.transform.position + (tmp * plusToPredict);
                }
                else
                {
                    posIni = target.transform.position + (tmp * plusToPredictTrebu);
                }
            }
            if (predict != maxFrameToPredict)
            {
                predict += 1;
            }
        } 
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
                towerPrice = (int)Enemy_Values_Gene.m_little_tower("m") / 2;
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

    private void lanzar()
    {
        if (type==1)
        {
            GameObject prefab = (GameObject) Resources.Load("Prefabs/defense1P_Rock_MT");
            GameObject p = Instantiate(prefab);
            p.AddComponent<Rigidbody>();
            p.transform.position = this.transform.GetChild(this.transform.childCount - 2).transform.position;
            p.AddComponent<ShootingMove>();
            p.GetComponent<ShootingMove>().pos = posIni;
            p.GetComponent<ShootingMove>().tag = "projectile";
        }
        else
        {
            GameObject p = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            p.AddComponent<Rigidbody>();
            Vector3 tmp = this.transform.position;
            tmp.y += 5f;
            p.transform.position = tmp;
            p.transform.localScale = new Vector3(2f, 2f, 2f);
            p.GetComponent<SphereCollider>().radius = 0.6f;
            p.AddComponent<ShootingMove>();
            p.GetComponent<ShootingMove>().pos = posIni;
            p.GetComponent<ShootingMove>().tag = "projectile";
        }
    }
}

