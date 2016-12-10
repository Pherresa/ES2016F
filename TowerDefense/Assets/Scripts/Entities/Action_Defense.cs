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
    Values valu;

    Animation anim;
    AnimationState stateTrebuchetIdle;
    AnimationState stateTrebuchetAttack;
    AnimationState stateTrebuchetrecharge;

    Animation[] anims;

    Enemy_Values_Gene generator_values;

    //private float timer = 1.5f;
    private int predict;
    private Vector3 posIni;
    private int maxFrameToPredict = 5;
    private int plusToPredictTrebu = 10;
    private int plusToPredict = 23;
    private int animationPhase = 0;
    private bool nextPhaseAnim = false;
    //private float speed = 2f;
    private bool isShooting = false;
    //int number = 0;
    bool couroutineStarted = false;
    int s = 3;
    DateTime timeOnPlay;
    // Funcion constructora por defecto. Inicializa variables.Aqui se debera leer de la BBDD i asignar
    // su valor a los respectivos atributos.

    void Awake() {
        getTypeOfDefense();
        generator_values = new Enemy_Values_Gene();
        generator_values.asig_values_tower(ref valu);
    }

    void Start()
    {
        
        //getValueTower();
        loadAnimations();
        initAnimations();
        iniStates();
    }

    private void loadAnimations()
    {
        switch (valu.type)
        {
            case TowerType.TREBUCHET_MT:
                anim = GetComponent<Animation>();
                break;
            case TowerType.MERCENARYHUMAN_I:
            case TowerType.ORCARCHER_I:
                anims = GetComponentsInChildren<Animation>();
                break;
        }
    }

    
    private void initAnimations()
    {
        switch (valu.type)
        {
            case TowerType.TREBUCHET_MT:
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
                break;

            case TowerType.MERCENARYHUMAN_I:
                //anim["A_Mercenary_idle"].speed = 1f;
                //anim["A_Mercenary_attack"].speed = 2f;

                break;

            case TowerType.ORCARCHER_I:
                //anim["A_OrcArcher_idle"].speed = 1f;
                anim["A_OrcArcher_attack"].speed = 2f;
                //anim["A_OrcArcher_attack"].speed = 2.5f;

                break;
        }
    }

    // inicializador
    void iniStates()
    {
        //BarrackRohanHorse
        if (active)
        {
            switch (valu.type)
            {
                case TowerType.ROHANBARRACKS_MT:
                    generateRohanHorses(2);
                    break;
            }
        }
    }

    private void checkPhaseAnim()
    {
        if (isShooting)
        {
            switch (valu.type)
            {
                case TowerType.TREBUCHET_MT:
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
                    break;
                case TowerType.MERCENARYHUMAN_I:
                    if (animationPhase == 1)
                    {
                        lanzar();
                        foreach (Animation animation in anims)
                        {
                            timeOnPlay = DateTime.Now;
                            animation.Play("A_Mercenary_attack");
                        }
                        animationPhase = 2;
                    }
                    else if (animationPhase == 2)
                    {
                        foreach (Animation animation in anims)
                        {
                            if ((DateTime.Now - timeOnPlay).Seconds > animation["A_Mercenary_attack"].length)
                            {
                                lanzar();
                                timeOnPlay = DateTime.Now;
                                anim.Play("A_Mercenary_recharge_");
                                animationPhase = 3;
                            }
                        }
                    }
                    else if (animationPhase == 3)
                    {
                        foreach (Animation animation in anims)
                        {
                            if ((DateTime.Now - timeOnPlay).Seconds > animation["A_Mercenary_recharge_"].length)
                            {
                                isShooting = false;
                                animationPhase = 0;
                                predict = 0;
                            }
                        }
                    }
                    break;
                case TowerType.ORCARCHER_I:
                    if (animationPhase == 1)
                    {
                        lanzar();
                        foreach (Animation animation in anims)
                        {
                            timeOnPlay = DateTime.Now;
                            animation.Play("A_OrcArcher_attack");
                        }
                        animationPhase = 2;
                    }
                    else if (animationPhase == 2)
                    {
                        foreach (Animation animation in anims)
                        {
                            if ((DateTime.Now - timeOnPlay).Seconds > animation["A_OrcArcher_attack"].length)
                            {
                                isShooting = false;
                                animationPhase = 0;
                                predict = 0;
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            switch (valu.type)
            {
                case TowerType.TREBUCHET_MT:
                    if (!anim.IsPlaying("A_Trebuchet_idle"))
                    {
                        anim.Play("A_Trebuchet_idle");
                    }
                    break;

                case TowerType.ORCARCHER_I:
                    foreach (Animation animation in anims)
                    {
                        if (!animation.IsPlaying("A_OrcArcher_idle"))
                        {
                            animation.Play("A_OrcArcher_idle");
                        }
                    }
                    break;

                case TowerType.MERCENARYHUMAN_I:
                    foreach (Animation animation in anims)
                    {
                        if (!animation.IsPlaying("A_Mercenary_idle"))
                        {
                            animation.Play("A_Mercenary_idle");
                        }
                    }
                    break;
            }
        }
    }

    private void checkDistanceTarget()
    {
        float distanceToEnemy = Vector3.Distance(this.transform.position, posIni);
        if (distanceToEnemy > valu.range)
        {
            target = null;
            animationPhase = 0;
            predict = 0;
            isShooting = false;
        }
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

    // funcion que se ejecuta continuamente.
    void FixedUpdate()
    {
        if (active)
        {
            if (!isShooting)
            {
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
                checkDistanceTarget();
                if (target != null)
                {
                    if (valu.type != TowerType.UNKNOWN && predict == maxFrameToPredict)
                    {
                        shoot();
                    }
                }


                // mientras no este puesto las animaciones de las demas torres fuerzo a que pasen por aqui para que disparen
                /*if (type != TowerType.TREBUCHET_MT && predict == maxFrameToPredict)
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
                else*/
            }

        }
    }

    
    // para definir el tipo de defensa que es (prefab) buscandolo por el nombre
    private void getTypeOfDefense()
    {
        String name = this.gameObject.name.Split('(')[0];
        if (name == "defense1_Trebuchet_MT")
        {
            valu.type = TowerType.TREBUCHET_MT;
        }
        else if (name == "defense2_RohanBarracks_MT")
        {
            valu.type = TowerType.ROHANBARRACKS_MT;
        }
        else if (name == "defense2_OrcArcher_I")
        {
            valu.type = TowerType.ORCARCHER_I;
        }
        else if (name == "defense3_MercenaryHuman_I")
        {
            valu.type = TowerType.MERCENARYHUMAN_I;
        }
        else if (name == "defense4_Aragorn_MT")
        {
            valu.type = TowerType.ARAGORN_MT;
        }
        else if (name == "defense5_Gandalf_MT")
        {
            valu.type = TowerType.GANDALF_MT;
        }
        else if (name == "defense4_Lurtz_I")
        {
            valu.type = TowerType.LURTZ_I;
        }
        else if (name == "OrcWarrior")
        {
            valu.type = TowerType.ORCWARRIOR;
        }
        else if (name == "defense1_Saruman_I")
        {
            valu.type = TowerType.SARUMAN_I;
        }
        else
        {
            valu.type = TowerType.UNKNOWN;
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
                if (valu.type == TowerType.TREBUCHET_MT)
                {
                    posIni = target.transform.position + (tmp * plusToPredictTrebu);
                }
                else
                {
                    posIni = target.transform.position + (tmp * plusToPredict);
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
        if (tmpEnemy != null && tmpDistance <= valu.range)
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

    /*private void getValueTower()
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
    }*/

    public int getTowerPrice()
    {
        return valu.towerPrice;
    }

    private void lanzar()
    {
        GameObject p;

        switch (valu.type)
        {
            case TowerType.TREBUCHET_MT:
                p = (GameObject)Resources.Load("Prefabs/defense1P_Rock_MT");
                p = Instantiate(p);
                p.AddComponent<Rigidbody>();
                p.transform.position = this.transform.GetChild(this.transform.childCount - 2).transform.position;
                p.AddComponent<ShootingMove>();
                p.GetComponent<ShootingMove>().pos = posIni;
                p.GetComponent<ShootingMove>().tag = "projectile";
                break;

            default:
                p = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                p.AddComponent<Rigidbody>();
                Vector3 tmp = this.transform.position;
                tmp.y += 5f;
                p.transform.position = tmp;
                p.transform.localScale = new Vector3(2f, 2f, 2f);
                p.GetComponent<SphereCollider>().radius = 0.6f;
                p.AddComponent<ShootingMove>();
                p.GetComponent<ShootingMove>().pos = posIni;
                p.GetComponent<ShootingMove>().tag = "projectile";
                break;
        }
    }


    void generateRohanHorses(int quantity)
    {

        GameObject rohanHorsePrefab = Resources.Load("Prefabs/defense2P_RohanHorse_MT") as GameObject;
        //GameObject rohanHorse;
        //Vector3 newPos;
        for (int i = 0; i < quantity; i++)
        {
            /*
			GameObject enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.transform.parent = transform;
			//get the thing component on your instantiated object
			AstarAI astarAI = enemy.GetComponent<AstarAI>();
			astarAI.speed = 12;*/

/*
            rohanHorse = Instantiate(rohanHorsePrefab);
            newPos = this.transform.position;
            if (i == 0) newPos.x -= 3;
            if (i == 2) newPos.x += 3;
            newPos.y -= 2;
            newPos.z += 2;
            rohanHorse.transform.position = newPos;
            //rohanHorse.AddComponent<Rigidbody>();
            rohanHorse.AddComponent<RohanHorse>();
            rohanHorse.GetComponent<RohanHorse>().center = this.transform.position;
            rohanHorse.GetComponent<RohanHorse>().tag = "projectile";

            //rohanHorse.transform.parent = transform;  */
        }

    }

    public Values getValues() {
        return valu;
    }
}

