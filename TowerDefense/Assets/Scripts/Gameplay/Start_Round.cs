using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Round : MonoBehaviour {

    public int act_time;
    public int total_round;
    

    private int cnt_time;
    private int cont_round;
    private bool act_time_cont;
    public TextMesh indicator_time;
    
    //public Text buttonText;

    // Use this for initialization
    void Start () {
        indicator_time = GameObject.FindGameObjectWithTag("Time_Cont").GetComponent<TextMesh>();
        indicator_time.text = act_time.ToString();
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
        indicator_time.text = act_time.ToString();
        if (act_time <= 0) // countdown_finish start game
        {
            new_round();
        }
    }

    private void new_round() {
        float time_tmp = cnt_time * 1.3f;
        act_time = (int)time_tmp;
        cnt_time = act_time;
        // Function Generate Round
        //Debug.Log("Generate Round");
    }
}
