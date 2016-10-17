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

    public Text amountText;
    public Text lifeText;
    public Text timeText;
    float remainingTime; //seconds

    // Use this for initialization
    void Start()
    {
        setRemainingTime(60f);
        amountText.text = "Amount: $" + amount.ToString();
        InvokeRepeating("decreaseTimeRemaining", 1f, 1f);


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
        amountText.text = "Amount: $" + amount.ToString();

    }
    void UpdateTimeText()
    {

        //TODO: minutes, sec fotmat
        minuteCount = (int)(remainingTime/60f);
        secCount = (int)(remainingTime%60f);
        timeText.text = minuteCount.ToString("00")+":"+ secCount.ToString("00");

    }

    public void LoseAmount(int a)
    {
        if (amount > 0)
        {
            amount -= a;
        }
        UpdateAmountText();
    }

    public void GainAmount(int a)
    {
        if (amount > 0)
        {
            amount += a;
        }
        UpdateAmountText();
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


}

