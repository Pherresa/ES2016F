using UnityEngine;

public class SellUpgradeUnit : MonoBehaviour {

    public GameObject selectedUnit = null;
    private CanvasGroup canvasSU;

    void Start()
    {
        
    }

    void OnMouseUp()
    {
        showCanvas();
    }

    public void showCanvas()
    {
        canvasSU = GameObject.Find("CanvasSU").GetComponent<CanvasGroup>();
        canvasSU.alpha = 1;
        canvasSU.interactable = true;
        canvasSU.blocksRaycasts = true;
        SellUnit su = GameObject.FindObjectOfType<SellUnit>();
        su.selectedUnit = this.selectedUnit;
        UpgradeUnit up = GameObject.FindObjectOfType<UpgradeUnit>();
        up.selectedUnit = this.selectedUnit;
    }

}
