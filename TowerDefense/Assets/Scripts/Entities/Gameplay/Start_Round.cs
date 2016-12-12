using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Round : MonoBehaviour {

    public enum Escen {
        ISENGARD,
        MINAS_TIRITH
    }

    public Escen type;

    private int act_time;
    public int total_round;
    

    private int cnt_time;
    private int cont_round;
    private bool act_time_cont;
    private GameManager indicator_time;
    private EnemyManager generate_round;
    public Game gameValues;
    private Enemy_Values_Gene values;
    private bool gameOver;
    
   
    
    
    //public Text buttonText;

    // Use this for initialization
    void Start () {
        values = new Enemy_Values_Gene();
        cont_round = 0;
        indicator_time = GameObject.Find("GameManager").GetComponent<GameManager>();
        indicator_time.setRemainingTime(0);
        generate_round = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        act_time = values.getTime();
        cnt_time = act_time;
        act_time_cont = true;
        gameOver = false;
        GameObject.Find("hordsText").GetComponent<Text>().text = "0/" + total_round;
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
                //GameObject.Find("timeText").GetComponent<Text>().font= (Font) Resources.Load("Fonts/RINGM___");
                //GameObject.Find("timeText").GetComponent<Text>().text = "Final Round";
                CancelInvoke();
            }
        }
    }

    private void new_round() { 
			
        float time_tmp = cnt_time * values.getTimeDecrement();
        act_time = (int)time_tmp;
        cnt_time = act_time;
        cont_round++;
        Debug.Log(cont_round);
        
        if (type == Escen.ISENGARD)
        {
            generate_round.createNewWaveIsengard(cont_round);
        }
        else {
            generate_round.createNewWaveMinasTirith(cont_round);
        }
        //Debug.Log("Generate Round");
        generate_round.createNewWave();
        GameObject.Find("hordsText").GetComponent<Text>().text = actu_round()+"/"+total_round;
		// After finishing a round, the currentScore is updated
		// to the finalScoreof the previos round
		GameManager lifeAM = GameObject.FindObjectOfType<GameManager>();
		
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
