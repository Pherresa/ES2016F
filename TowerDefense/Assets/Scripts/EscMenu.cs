using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour {

    
    public void mContinueGame()
    {   
        Time.timeScale = 1.0f;
        GameObject tmp = GameObject.Find("escMenu");
        tmp.SetActive(false);
    }

    public void mSaveGame() 
    {
        SaveLoad.SaveData();
    }

    public void mExitGame()
    {
        print("exit");
        //mEnablePanel("initMenu");
        Application.Quit();
    }
    public void mReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void mSubmit(){
        Application.Quit();
    }


}

