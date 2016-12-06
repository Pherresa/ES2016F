using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour {

    
    public void mContinueGame()
    {   
        Time.timeScale = 1.0f;
        GameObject tmp = GameObject.FindGameObjectWithTag("gameExit");
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

}

