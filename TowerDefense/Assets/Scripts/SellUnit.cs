using UnityEngine;
using System.Collections;
using System;

public class SellUnit : MonoBehaviour {

    public GameObject selectedUnit = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SellSelected()
    {
        Debug.Log("Sell Selected");
        GameObject empty = (GameObject) Resources.Load("prefabs/Empty", typeof(GameObject));
        GameObject newObj = (GameObject) Instantiate(empty, selectedUnit.transform.position - new Vector3(0, 0.5f, 0), selectedUnit.transform.rotation);

        newObj.AddComponent(Type.GetType("PlaceUnit"));

        Destroy(selectedUnit);
        selectedUnit = null;

        GameObject canvasSU = GameObject.Find("CanvasSU");
        canvasSU.GetComponent<CanvasGroup>().alpha = 0;
        canvasSU.GetComponent<CanvasGroup>().interactable = false;
        canvasSU.GetComponent<CanvasGroup>().blocksRaycasts = false;

        //TO DO: Money back?
        GameObject.FindObjectOfType<LifeAmountManager>().GainAmount(40);
    }
}
