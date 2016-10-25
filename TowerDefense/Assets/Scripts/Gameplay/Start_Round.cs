using UnityEngine;
using System.Collections;

public class Start_Round : MonoBehaviour {

    public int act_time;

    private GameObject indicator_time;
    private float cnt_time;

    // Use this for initialization
    void Start () {
        Time.timeScale = 0.0f;
        cnt_time = 0.0f;
        indicator_time = GameObject.FindGameObjectWithTag("Time_Cont");
    }

    // Update is called once per frame
    void Update() {
        cnt_time += Time.fixedDeltaTime;
        if (cnt_time >= 1.0f) {
            act_time -= 1;
            if (act_time == 0)
            {
                GameObject.Destroy(indicator_time);
                Time.timeScale = 1.0f;
            }else {
                indicator_time.GetComponent<TextMesh>().text = act_time.ToString();
            }
            cnt_time = 0.0f;
        }
    }

	public void OnMouseUpAsButton() {
        GameObject.Destroy(indicator_time);
        Time.timeScale = 1.0f;
    }
}
