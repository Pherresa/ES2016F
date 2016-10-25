using UnityEngine;
using System.Collections;

public class Start_Round : MonoBehaviour {

    public int act_time;
    private GameObject indicator_time;
    private float cnt_time;

	// Use this for initialization
	void Start () {
        //Time.timeScale = 0.0f;
        cnt_time = 0.0f;
        indicator_time = GameObject.FindGameObjectWithTag("Time_Cont");
        indicator_time.GetComponent<TextMesh>().text = act_time.ToString();
    }

    // Update is called once per frame
    void Update(){
    }

	void FixedUpdate () {
        cnt_time += Time.deltaTime;
        if (cnt_time >= 1.0f)
        {
            int in_time = int.Parse(indicator_time.GetComponent<TextMesh>().text) - 1;
            indicator_time.GetComponent<TextMesh>().text = in_time.ToString();
            cnt_time = 0.0f;
        }
    }
}
