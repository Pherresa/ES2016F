using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Round : MonoBehaviour {

    public int act_time;
    public int total_round;
    

    private int cnt_time;
    private int cont_round;
    private bool act_time_cont;
    private LifeAmountManager indicator_time;
    private EnemyManager generate_round;
    
    //public Text buttonText;

    // Use this for initialization
    void Start () {
        indicator_time = GameObject.Find("GameManager").GetComponent<LifeAmountManager>();
        indicator_time.setRemainingTime(0);
        generate_round = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        cnt_time = act_time;
        act_time_cont = true;
    }

    // Update is called once per frame
    void Update() {
    }

    public void OnMouseUpAsButton() {
        new_round();
        if (act_time_cont) {
            InvokeRepeating("countDown", 0f, 1f); // active countdown
            act_time_cont = false;
        }
    }

    private void countDown() {
        act_time -= 1;
        indicator_time.setRemainingTime(act_time);
        if (act_time <= 0) // countdown_finish start game
        {
            new_round();
        }
    }

    private void new_round() {
        float time_tmp = cnt_time * 1.3f;
        act_time = (int)time_tmp;
        cnt_time = act_time;
        generate_round.createNewWave();
        //Debug.Log("Generate Round");
    }
}
