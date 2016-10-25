using UnityEngine;
using System.Collections;

public class Start_Round : MonoBehaviour {

    GameObject indicator_time;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
        indicator_time = GameObject.FindGameObjectWithTag("Time_Cont");
        indicator_time.GetComponent<TextMesh>().text = "60";
    }

    // Update is called once per frame
    void Update(){
    }

	void FixedUpdate () {
        int in_time = int.Parse(indicator_time.GetComponent<TextMesh>().text)-1;
        indicator_time.GetComponent<TextMesh>().text = in_time.ToString();
    }
}
