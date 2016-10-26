using UnityEngine;
using System.Collections;

public class Start_Round : MonoBehaviour {

    public int act_time;

    private GameObject indicator_time;
    private float cnt_time;
    private bool cnt_time_end; // When it reaches zero is activated

    // Use this for initialization
    void Start () {
        Time.timeScale = 0.0f;
        cnt_time = 0.0f;
        cnt_time_end = false;
        indicator_time = GameObject.FindGameObjectWithTag("Time_Cont"); // we seek object label
    }

    // Update is called once per frame
    void Update() {
        cnt_time += Time.fixedDeltaTime; // We count the time between frame
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
        }
    }

	public void OnMouseUpAsButton() {
        if (cnt_time_end) { //switch to play or pause
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                this.gameObject.GetComponent<TextMesh>().text = "Pause";
            }
            else
            {
                Time.timeScale = 0.0f;
                this.gameObject.GetComponent<TextMesh>().text = "Play";
            }
        }
        else // If you activate the button before the time runs out the game begins
        { 
            GameObject.Destroy(indicator_time);
            Time.timeScale = 1.0f;
            cnt_time_end = true;
            this.gameObject.GetComponent<TextMesh>().text = "Pause";
        }
    }
}
