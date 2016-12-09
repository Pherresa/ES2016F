using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Round : MonoBehaviour {

    private int act_time;
    public int total_round;
    

    private int cnt_time;
    private int cont_round;
    private bool act_time_cont;
    private LifeAmountManager indicator_time;
    private EnemyManager generate_round;
    public Game gameValues;
    private bool gameOver;
    
   
    
    
    //public Text buttonText;

    // Use this for initialization
    void Start () {
        cont_round = 0;
        indicator_time = GameObject.Find("GameManager").GetComponent<LifeAmountManager>();
        indicator_time.setRemainingTime(0);
        generate_round = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        act_time = Enemy_Constants.TIME;
        cnt_time = act_time;
        act_time_cont = true;
        gameOver = false;
    }

    // Update is called once per frame
    void Update() {
    }

    public void OnMouseUpAsButton() {
        new_round();
        if (act_time_cont) {
            InvokeRepeating("countDown", 0f, 1f); // active countdown
            act_time_cont = false;
            GameObject.Find("Play").SetActive(false);
        }
    }

    private void countDown() {
        act_time -= 1;
        indicator_time.setRemainingTime(act_time);
        if (!gameOver)
        { 
            if (act_time <= 0 && cont_round < total_round) // countdown_finish start game
            {
                new_round();
            }
            if (cont_round >= total_round) {
                // Poner Final Round
                indicator_time.set_final_round(true);
                GameObject.Find("timeText").GetComponent<Text>().font= (Font) Resources.Load("Fonts/RINGM___");
                GameObject.Find("timeText").GetComponent<Text>().text = "Final Round";
                CancelInvoke();
            }
        }
    }

    private void new_round() { 
			
        float time_tmp = cnt_time * Enemy_Constants.TIME_DECREASE;
        act_time = (int)time_tmp;
        cnt_time = act_time;
        cont_round++;
        Debug.Log(cont_round);
        generate_round.createNewWave();
        //Debug.Log("Generate Round");
        GameObject.Find("level").GetComponent<Text>().text = "LEVEL " + actu_round();
		// After finishing a round, the currentScore is updated
		// to the finalScoreof the previos round
		LifeAmountManager lifeAM = GameObject.FindObjectOfType<LifeAmountManager>();
		
        //lifeAM.currentScore = lifeAM.currentScoreNextRound;
    }

    public int actu_round(){
        return cont_round;
    }

    public void setGameOver()
    {
        gameOver = true;
    }

    public bool getGameOver()
    {
        return gameOver;
    }

}
