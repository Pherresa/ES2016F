using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LifeAmountManager : MonoBehaviour
{

    public int life = 1000; // TODO: Initial life value?
    public int amount = 44; // TODO: Initial money value?
    private float startTime; // Used for the timer
    private int minuteCount;
    private int secCount;

    private bool newSec;
    private GeneralEnemy[] enemies;


    public Text amountText;
    public Text lifeText;
    public Text timeText;
    float remainingTime; //seconds

    // Use this for initialization
    void Start()
    {
        newSec = false;
        enemies = FindObjectsOfType(typeof(GeneralEnemy)) as GeneralEnemy[]; 
        //setRemainingTime(60f);
        amountText.text = amount.ToString();
        //InvokeRepeating("decreaseTimeRemaining", 1f, 1f);
        UpdateAvailableUnits();

    }

    public void setRemainingTime(float r)
    {
        remainingTime = r;
    }

    void UpdateLifeText()
    {

        lifeText.text = "Tower Life: " + life.ToString();

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
        timeText.text = minuteCount.ToString("00")+":"+ secCount.ToString("00");
        // For example make enemies life down by time
        foreach (var en in enemies){
            if(en.alive==true){
                if(secCount>0 && newSec){
                   en.downLife((60-secCount));
                }
            }
        }
    }

    public void LoseAmount(int a)
    {
        if (amount > 0)
        {
            amount -= a;
        }
        UpdateAmountText();
        UpdateAvailableUnits();
    }

    public void GainAmount(int a)
    {
        if (amount > 0)
        {
            amount += a;
        }
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
        if (amount >= 150) //TO DO: COST OF UNIT 1
        {
            GameObject.Find("ImageUnit1").GetComponent<Image>().color = Color.red;
        }
        else
        {
            GameObject.Find("ImageUnit1").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit1").GetComponent<Button>().enabled = false;
        }
        if (amount >= 450) //TO DO: COST OF UNIT 2
        {
            GameObject.Find("ImageUnit2").GetComponent<Image>().color = Color.blue;
        }
        else
        {
            GameObject.Find("ImageUnit2").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit2").GetComponent<Button>().enabled = false;
        }
        if (amount >= 700) //TO DO: COST OF UNIT 3
        {
            GameObject.Find("ImageUnit3").GetComponent<Image>().color = Color.green;
        }
        else
        {
            GameObject.Find("ImageUnit3").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit3").GetComponent<Button>().enabled = false;
        }
        if (amount >= 900) //TO DO: COST OF UNIT 4
        {
            GameObject.Find("ImageUnit4").GetComponent<Image>().color = Color.magenta;
        }
        else
        {
            GameObject.Find("ImageUnit4").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit4").GetComponent<Button>().enabled = false;
        }
        if (amount >= 1000) //TO DO: COST OF UNIT 5
        {
            GameObject.Find("ImageUnit5").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("ImageUnit5").GetComponent<Image>().color = Color.gray;
            GameObject.Find("ButtonUnit5").GetComponent<Button>().enabled = false;
        }

    }


}

