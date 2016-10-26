using UnityEngine;

/*
 * SellUpgradeUnit class controles the sell and Upgrade buttons. Each unit has associated a Script sellUpgradeUnit 
 * 
*/
public class SellUpgradeUnit : MonoBehaviour {

    public GameObject selectedUnit = null;
    private CanvasGroup canvasSU;

    void Start()
    {
        
    }

	// OnMouseUp will call the showCanvas function.
    void OnMouseUp()
    {
        showCanvas();
    }

	//ShowCavas method that show the buttons sell and upgrade
    public void showCanvas()
    {
		//CanvasSU contains both upgrade button and sell button 
        canvasSU = GameObject.Find("CanvasSU").GetComponent<CanvasGroup>();
		//Set the transparence of the canvasSU, the bigger the alhpa is the less transparent is the canvasSU
        canvasSU.alpha = 1.0f;
		//Allows the user interact with the canvasSU
        canvasSU.interactable = true;
        canvasSU.blocksRaycasts = true;
		//Find a sellUnit instance
        SellUnit su = GameObject.FindObjectOfType<SellUnit>();
		//Get to know the SellUnit the last unit which has been selected.
        su.selectedUnit = this.selectedUnit;
		//Find an upgradeUnit instance
        UpgradeUnit up = GameObject.FindObjectOfType<UpgradeUnit>();
		//Get to know the UpgradeUnit the last unit which has been selected.
        up.selectedUnit = this.selectedUnit;
    }

}
