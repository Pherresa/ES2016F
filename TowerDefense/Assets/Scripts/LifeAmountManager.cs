using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LifeAmountManager : MonoBehaviour
{

    public static int FIRST_TURRET_PRICE = 10;
    public static int SECOND_TURRET_PRICE = 20;
    public static int THIRTH_TURRET_PRICE = 20;
    public static int FOURTH_TURRET_PRICE = 25;
    public static int FITH_TURRET_PRICE = 30;

    public int life = 1000; // TODO: Initial life value?
    public int amount = 200; // TODO: Initial money value?
    private float startTime; // Used for the timer
    private int minuteCount;
    private int secCount;

    private bool newSec;
    private GeneralEnemy[] enemies;
    private bool final_round;


    public Text amountText;
    public Text lifeText;
    public Text timeText;
    public float remainingTime; //seconds

    // Use this for initialization
    void Start()
    {
        amount = Enemy_Constants.WALLET;
        life = Enemy_Values_Gene.m_mt_tower("l");
        UpdateLifeText();
        FIRST_TURRET_PRICE = Enemy_Values_Gene.m_little_tower("m");
        SECOND_TURRET_PRICE = Enemy_Values_Gene.m_medium_tower("m");
        THIRTH_TURRET_PRICE = Enemy_Values_Gene.m_big_tower("m");
        newSec = false;
        final_round = false;
        enemies = FindObjectsOfType(typeof(GeneralEnemy)) as GeneralEnemy[]; 
        //setRemainingTime(60f);
        amountText.text = amount.ToString();
        //InvokeRepeating("decreaseTimeRemaining", 1f, 1f);

    }

    public void setRemainingTime(float r)
    {
        remainingTime = r;
    }

    void UpdateLifeText()
    {

        lifeText.text = life.ToString();

    }
    void UpdateAmountText()
    {
        amountText.text = amount.ToString();

    }
    void UpdateTimeText()
    {
        minuteCount = (int)(remainingTime/60f);
        if(secCount!=(int)(remainingTime%60f)){
            newSec = true;
        }
        else{
            newSec = false;
        }
        secCount = (int)(remainingTime%60f);
        if(!final_round)
            timeText.text = minuteCount.ToString("00")+":"+ secCount.ToString("00");

    }

    public bool LoseAmount(int a)
    {
        if (amount >= a)
        {
            amount -= a;
            UpdateAmountText();
            UpdateAvailableUnits();

            return true;
        }
        return false;
    }

    public void GainAmount(int a)
    {
        amount += a;
        UpdateAmountText();
        UpdateAvailableUnits();
    }

    public void LoseLife(int l = 1)
    {
        life -= l;
        if (life <= 0)
        {
            Die();
        }

        UpdateLifeText();
    }

    public void Die()
    {
        Debug.Log("Game Over");
    }

    public void decreaseTimeRemaining()
    {
        remainingTime--;
       
        if (remainingTime < 0)
        {
            //TODO: Time between waves?
            remainingTime = 60f;
        }
        UpdateTimeText();
    }

    void Update()
    {
        UpdateTimeText();
    }

    void UpdateAvailableUnits()
    {
        if (amount >= FIRST_TURRET_PRICE) //TO DO: COST OF UNIT 1
        {
            GameObject.Find("ImageUnit1").GetComponent<Image>().color = Color.red;
        }
        else
        {
            GameObject.Find("ImageUnit1").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit1").GetComponent<Button>().enabled = false;
        }
        if (amount >= SECOND_TURRET_PRICE) //TO DO: COST OF UNIT 2
        {
            GameObject.Find("ImageUnit2").GetComponent<Image>().color = Color.blue;
        }
        else
        {
            GameObject.Find("ImageUnit2").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit2").GetComponent<Button>().enabled = false;
        }
        if (amount >= THIRTH_TURRET_PRICE) //TO DO: COST OF UNIT 3
        {
            GameObject.Find("ImageUnit3").GetComponent<Image>().color = Color.green;
        }
        else
        {
            GameObject.Find("ImageUnit3").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit3").GetComponent<Button>().enabled = false;
        }
        if (amount >= FOURTH_TURRET_PRICE) //TO DO: COST OF UNIT 4
        {
            GameObject.Find("ImageUnit4").GetComponent<Image>().color = Color.magenta;
        }
        else
        {
            GameObject.Find("ImageUnit4").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit4").GetComponent<Button>().enabled = false;
        }
        if (amount >= FITH_TURRET_PRICE) //TO DO: COST OF UNIT 5
        {
            GameObject.Find("ImageUnit5").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("ImageUnit5").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit5").GetComponent<Button>().enabled = false;
        }

    }

    public void set_final_round(bool e) {
        final_round = e;
    }
}

