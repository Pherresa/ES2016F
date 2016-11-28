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

	public AudioClip click;
	private AudioSource source {
		get{
			return GetComponent<AudioSource> ();

		}
	}


    // Use this for initialization

    private static string path = "";
    private static string actualPanelTag = "initMenu";

    void Start () {
        Time.timeScale = 1.0f;
        mEnablePanel("initMenu");
		gameObject.AddComponent<AudioSource> ();
		source.clip = click;
		source.playOnAwake = false;


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

	void playSound(){
		source.PlayOneShot (click);
	}
    public void OnMouseUpAsButton()
    {
        if (isStartButton)
        {
			Debug.Log("Pulsando start button");
			playSound ();
            path += "start|";
            mEnablePanel("battleMenu");            
            //print(path + " -------> " + actualPanelTag);
        }

        if (isOptionButton)
        {

			Debug.Log("Pulsando option button");
			playSound ();
            path += "option|";
            mEnablePanel("optMenu");
            //print(path + " -------> " + actualPanelTag);
        }

        if (isExitButton)
        {            
			Debug.Log("Pulsando exit button");
			playSound ();
            //mEnablePanel("initMenu");
            Application.Quit();
        }

        if (isReturnMMButton)
        {
			Debug.Log("Pulsando return button");
			playSound ();
			mEnablePanel("initMenu");
			path = "";
            //print(path + " -------> " + actualPanelTag);
			isReturnMMButton = true;
        }

        if (isBattleTirithButton)
        {
            //mEnablePanel("gameModeMenu");
			playSound ();
            path += "tiri|";
            //print(path + " -------> " + actualPanelTag);

            SceneManager.LoadScene(2);
            print("Cargando Tirith");
        }

        if (isBattleIsengardButton)
        {
            //mEnablePanel("gameModeMenu");
			playSound ();
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
			Debug.Log("Pulsando load button");
			playSound ();
            mEnablePanel("initMenu");
            path = "load|";

            //print(path + " -------> " + actualPanelTag);
        }





    }


}
