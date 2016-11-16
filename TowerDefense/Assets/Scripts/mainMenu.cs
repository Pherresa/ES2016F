using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    public bool isStartButton = false;
    public bool isOptionButton = false;
    public bool isExitButton = false;
    public bool isReturnMMButton = false;
    public bool isBattleTirithButton = false;
    public bool isBattleIsengardButton = false;
    public bool isArcadeGameButton = false;
    public bool isAdventureGameButton = false;
    public bool isLoadGameButton = false;


    // Use this for initialization

    private static string path = "";
    private static string actualPanelTag = "initMenu";

    void Start () {
        mEnablePanel("initMenu");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mEnablePanel("initMenu");
            path = "";
        }
        
      
    }


    private void mEnablePanel(string tagNext)
    {
        GameObject tmp = GameObject.FindGameObjectWithTag(actualPanelTag);
        Animator animTmp = tmp.GetComponent<Animator>();
        animTmp.enabled = false;
        tmp.transform.Translate(100000, 0, 0);

        GameObject goActual = GameObject.FindGameObjectWithTag(tagNext);
        Animator animActual = goActual.GetComponent<Animator>();
        animActual.enabled = true;
        animActual.Play(0);

        actualPanelTag = tagNext;
        
    }

    public void OnMouseUpAsButton()
    {
        if (isStartButton)
        {
            path += "start|";
            mEnablePanel("battleMenu");            
            //print(path + " -------> " + actualPanelTag);
        }

        if (isOptionButton)
        {
            path += "option|";
            mEnablePanel("optMenu");
            //print(path + " -------> " + actualPanelTag);
        }

        if (isExitButton)
        {            
            print("exit");
            //mEnablePanel("initMenu");
            Application.Quit();
        }

        if (isReturnMMButton)
        {
            mEnablePanel("initMenu");
            path = "";
            //print(path + " -------> " + actualPanelTag);
        }

        if (isBattleTirithButton)
        {
            //mEnablePanel("gameModeMenu");
            path += "tiri|";
            //print(path + " -------> " + actualPanelTag);

            SceneManager.LoadScene(2);
            print("Cargando Tirith");
        }

        if (isBattleIsengardButton)
        {
            //mEnablePanel("gameModeMenu");
            path += "isen|";
            //print(path + " -------> " + actualPanelTag);
            SceneManager.LoadScene(1);
            print("Cargando isengard");


        }

        if (isArcadeGameButton)
        {
            mEnablePanel("gameModeMenu");
            path += "arcad";
            //Save path
            //print(path + " -------> " + actualPanelTag);
            string[] sPath = path.Split('|');
            if(sPath.Length >= 3)
            {
                if(sPath[1].Equals("isen"))
                {
                    SceneManager.LoadScene(1);
                    print("Cargando Arcade isengard");
                }
                else
                {
                    SceneManager.LoadScene(2);
                    print("Cargando Arcade Tirith");
                }
            }
            
        }

        if (isAdventureGameButton)
        {
            path += "adven";
            //Save path
            //print(path + " -------> " + actualPanelTag);

            string[] sPath = path.Split('|');
            if (sPath.Length >= 3)
            {
                if (sPath[1].Equals("isen"))
                {
                    SceneManager.LoadScene(1);
                    print("Cargando Adventurer isengard");
                }
                else
                {
                    SceneManager.LoadScene(2);
                    print("Cargando adventurer Tirith");
                }
            }
        }

        if (isLoadGameButton)
        {
            mEnablePanel("initMenu");
            path = "load|";

            //print(path + " -------> " + actualPanelTag);
        }





    }


}
