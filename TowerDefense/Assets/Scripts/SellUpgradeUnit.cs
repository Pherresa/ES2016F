using UnityEngine;

public class SellUpgradeUnit : MonoBehaviour {

    public GameObject selectedUnit = null;

    void OnMouseUp()
    {
        //GameObject selectedUnit = transform.parent.gameObject;
        

    }

    public void showCanvas()
    {
        GameObject canvasSU = GameObject.Find("CanvasSU");
        canvasSU.GetComponent<CanvasGroup>().alpha = 1;
        canvasSU.GetComponent<CanvasGroup>().interactable = true;
        canvasSU.GetComponent<CanvasGroup>().blocksRaycasts = true;
        SellUnit su = GameObject.FindObjectOfType<SellUnit>();
        su.selectedUnit = this.selectedUnit;
        UpgradeUnit up = GameObject.FindObjectOfType<UpgradeUnit>();
        up.selectedUnit = this.selectedUnit;
    }

}
