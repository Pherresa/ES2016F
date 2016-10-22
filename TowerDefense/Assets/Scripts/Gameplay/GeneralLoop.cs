using UnityEngine;
using System.Collections;

public class GeneralLoop : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
	}


    // Update is called once per frame
    // Get all the enemies, increases the z position of each of them
    void Update () {
    }

    // Pressing the button the value of the variable changes
    // To make the pause, the methods have to be in the FixedUpdate functions, or update using Time
    public void OnMouseUpAsButton()
    {
        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }
}
