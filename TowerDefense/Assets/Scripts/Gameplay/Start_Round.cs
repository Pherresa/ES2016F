using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Round : MonoBehaviour {

    public int act_time;

   
    private float cnt_time;
    private bool cnt_time_end; // When it reaches zero is activated
    public Text indicator_time;
    public Text buttonText;

    // Use this for initialization
    void Start () {
        //Time.timeScale = 0.0f;
        cnt_time = 0.0f;
        cnt_time_end = false;
        InvokeRepeating("countDown", 0f, 1f);
    }

    // Update is called once per frame
    void Update() {
        /*cnt_time += Time.fixedDeltaTime; // We count the time between frame
        if (cnt_time >= 1.0f)
        {
            act_time -= 1;
            if (act_time == 0) // countdown_finish start game
            {
                GameObject.Destroy(indicator_time);
                Time.timeScale = 1.0f;
                cnt_time_end = true;
                this.gameObject.GetComponent<TextMesh>().text = "Pause";
            }
            else
            {
                indicator_time.GetComponent<TextMesh>().text = act_time.ToString();
            }
            cnt_time = 0.0f;
        }*/
    }

	public void play_or_pause() {
        if (cnt_time_end) { //switch to play or pause
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                buttonText.text = "Pause";
            }
            else
            {
                Time.timeScale = 0.0f;
                buttonText.text = "Play";
            }
        }
        else // If you activate the button before the time runs out the game begins
        { 
            GameObject.Destroy(indicator_time);
            Time.timeScale = 1.0f;
            cnt_time_end = true;
            buttonText.text = "Pause";
        }
    }

    private void countDown() {
        act_time -= 1;
        if (act_time == 0) // countdown_finish start game
        {
            GameObject.Destroy(indicator_time);
            Time.timeScale = 1.0f;
            cnt_time_end = true;
            buttonText.text = "Pause";
        }
        else
        {
            indicator_time.text = act_time.ToString();
        }
    }
}
