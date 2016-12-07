using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
[System.Serializable]
public class LifeAmountManager : MonoBehaviour
{

    public static int FIRST_TURRET_PRICE = 10;
    public static int SECOND_TURRET_PRICE = 20;
    public static int THIRTH_TURRET_PRICE = 20;
    public static int FOURTH_TURRET_PRICE = 25;
    public static int FITH_TURRET_PRICE = 30;

    public int life = 1000; // TODO: Initial life value?
    public int amount = 200; // TODO: Initial money value?
    public int finalScore = 0;

	public int currentScore = 0; // TODO: TEAM_D show in the play window
	// This will use to reset the score 
	// after finishing a round (Start_Round.cs)
	public int currentScoreNextRound = 0; 

    private float startTime; // Used for the timer
    private int minuteCount;
    private int secCount;

    private bool newSec;
    private GeneralEnemy[] enemies;
    private bool final_round;

    private Start_Round start_round;
    private GameObject[] enemiesToDestroy;


    public Text scoreText;
    public Text amountText;
    public Text lifeText;
    public Text timeText;
    public float remainingTime; //seconds
    public GameObject mainTower;
    public GameObject firstD;
    public GameObject secondD;
    public GameObject thirdD;

    public GameObject endMenu;



    // Use this for initialization
    void Start()
    {
        endMenu.SetActive(false);
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
        start_round = GameObject.FindObjectOfType<Start_Round>();
        



    }

    public void setRemainingTime(float r)
    {
		remainingTime = r;
    }

    public void UpdateLifeText()
    {

        lifeText.text = life.ToString();

    }
    public void UpdateAmountText()
    {
        amountText.text = amount.ToString();

    }

    public void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
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
        checkLife();
        life -= l;

        if (life <= Enemy_Values_Gene.m_mt_tower("l")- Enemy_Values_Gene.m_mt_tower("l")*0.25 && life >= Enemy_Values_Gene.m_mt_tower("l") - Enemy_Values_Gene.m_mt_tower("l") * 0.5)
        {
            firstD.SetActive(true);

        }
        else if (life <= Enemy_Values_Gene.m_mt_tower("l") - Enemy_Values_Gene.m_mt_tower("l") * 0.5 && life >= Enemy_Values_Gene.m_mt_tower("l") - Enemy_Values_Gene.m_mt_tower("l") * 0.75)
        {
            secondD.SetActive(true);
        }
        else if (life <= Enemy_Values_Gene.m_mt_tower("l") - Enemy_Values_Gene.m_mt_tower("l") * 0.75 && life >= 0)
        {
            thirdD.SetActive(true);
        }
    }


    /*
     * Method to check if life is 0 or less 
     */
    private void checkLife()
    {
        if (life < 0)
        {
            start_round.setGameOver();
            Die();
        }
        else
        {
            UpdateLifeText();
        }
    }
    public void Die()
    {
        Debug.Log("Game Over");
        enemiesT
        endMenu.SetActive(true);
        Text finalScoreText = GameObject.Find("finalScoreText").GetComponent<Text>();
        string txt = "Your final score is " + currentScore.ToString();
        finalScoreText.text = txt;
        //Time.timeScale = 0;
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
        UpdateScoreText();
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


	/**
	 * The formula will calculate after the player die or after the player finished a specific level (round).
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
		int priceObjects = calculatePriceBoughtObjects(); //  Get the price of the bought objects 

		// the level also is a good parameter to obtain the final score -> level 
		Start_Round st = GameObject.FindObjectOfType<Start_Round>();
		int level = 1; 					// Get the level of the game 
									    // We do +1 because it's start in 0.
		if (st != null) {level += st.actu_round();}


		Debug.Log ("remainingRime");
		Debug.Log((int)(remainingTime));

		// We save the value of the finalScore becuase in the next 
		// round this score will be the current score of the player.
		// So now we can define the formula: 
		currentScoreNextRound = weight * level * (life + ((int)(remainingTime))) + amount + priceObjects + currentScore;
        finalScore = currentScoreNextRound;
		return currentScoreNextRound;
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

