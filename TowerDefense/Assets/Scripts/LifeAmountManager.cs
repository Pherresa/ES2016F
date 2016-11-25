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
	public int currentScore = 0; // TODO: 
    private float startTime; // Used for the timer
    private int minuteCount;
    private int secCount;

    private bool newSec;
    private GeneralEnemy[] enemies;


    public Text amountText;
    public Text lifeText;
    public Text timeText;
    public float remainingTime; //seconds

    // Use this for initialization
    void Start()
    {
        newSec = false;
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
		Debug.Log("TEXTime");
		Debug.Log(calculateFinalScore());

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


	/**
	 * The formula will calculate after the player die or after the player finished a specific level.
	 * This will depend to the life's player, time's remaining, money' remaining and the objects you bought.
	 */
	public int calculateFinalScore(){

		// weight value: we give 5 to balance the final score, most reality
		int weight = 5;

		// life parameter we will use it also for the formula -> life 
		// the time remaining is important too  -> timeremaining  
		// player's money remaining is used too -> amount

		// objects that players bougth aslo -> priceObjects 
		Debug.Log ("Price objects Final");
		int priceObjects = calculatePriceBoughtObjects(); // TODO: Get the price of the bought objects 

		// the level also is a good parameter to obtain the final score -> level 
		int level = 1; // TODO: Get the level of the game

		Debug.Log ("remainingRime");
		Debug.Log((int)(remainingTime));
		// So now we can define the formula 
		return weight * level * (life + ((int)(remainingTime))) + amount + priceObjects + currentScore;
	}

	/**
	 * Calculates the price of bought objects.
	 */
	public int calculatePriceBoughtObjects() {
		Action_Defense[] objects = (Action_Defense[])GameObject.FindObjectsOfType<Action_Defense> ();
		Debug.Log ("Price objects 0");
		int priceObjects = 0;
		for (int i = 0; i < objects.Length; i++) {
			Debug.Log (objects [i].towerPrice);
			priceObjects += objects [i].towerPrice;
		}
		return priceObjects / 2;
	}


	/**
	 * Update value of current score
	 * 
	 */
	public void updateCurrentScore( int value ) {
		this.currentScore += value;
	}



}

