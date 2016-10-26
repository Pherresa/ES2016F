using UnityEngine;
using System.Collections;
using System;

/*
 * SellUnit class handles the sell of the units.
 */
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
		//Load a gameobject from prefabs/Empty
        GameObject empty = (GameObject) Resources.Load("prefabs/Empty", typeof(GameObject));
		//Instantiate tha empty gameobject setting the position and rotation of this new object.
        GameObject newObj = (GameObject) Instantiate(empty, selectedUnit.transform.position - new Vector3(0, 0.5f, 0), selectedUnit.transform.rotation);

		//Add the PlaceUnit component to the newObj
        newObj.AddComponent(Type.GetType("PlaceUnit"));

		//destroy the selectedUnit we have sold.
        Destroy(selectedUnit);
        selectedUnit = null;

		//Find a canvasSU object
        GameObject canvasSU = GameObject.Find("CanvasSU");
		//Get the sell canvasSu component and set the alpha, interactable and  blockRaycasts values in order to disable the sellButton
        canvasSU.GetComponent<CanvasGroup>().alpha = 0;
        canvasSU.GetComponent<CanvasGroup>().interactable = false;
        canvasSU.GetComponent<CanvasGroup>().blocksRaycasts = false;

        //TO DO: Money back?
        GameObject.FindObjectOfType<LifeAmountManager>().GainAmount(40);
    }
}
