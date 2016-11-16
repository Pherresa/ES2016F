using UnityEngine;
using System.Collections;

public class KeyEvnts : MonoBehaviour {

    public GameObject escMenu;
    private bool escMenuIsActive = false;


	// Use this for initialization
	void Start () {
        escMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenuIsActive)
            {
                Time.timeScale = 1.0f;
                escMenu.SetActive(false);
                escMenuIsActive = false;
            }
            else
            {
                escMenu.SetActive(true);
                Time.timeScale = 0.0f;
                escMenuIsActive = true;

}
        }
    }
}
