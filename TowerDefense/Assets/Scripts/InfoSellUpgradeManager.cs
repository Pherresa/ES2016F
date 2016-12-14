using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InfoSellUpgradeManager : MonoBehaviour {

	private CanvasGroup canvasSU;
	private Text infoUnitText;
	private CanvasGroup canvasIU;
    private CanvasGroup HUDCanvas;
    private Button sell;
	MouseManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager> ();
		canvasSU = GameObject.Find("CanvasSU").GetComponent<CanvasGroup>();
		infoUnitText = GameObject.Find("infoUnitText").GetComponent<Text>();
		canvasIU = GameObject.Find("InfoUnit").GetComponent<CanvasGroup>();
		setActive (false);
        sell = GameObject.Find("ButtonSell").GetComponent();
    }

	// Update is called once per frame
	void Update () {
		if (mm.selectedObject != null){
			setActive (true);
		} else {
			setActive (false);
		}
	}

	void setActive(bool en){
		if (en) {
			canvasSU.alpha = 1;
			canvasSU.interactable = true;
			canvasSU.blocksRaycasts = true;

			Vector3 newPositionSU = Camera.main.WorldToScreenPoint (mm.selectedObject.transform.position);
			Vector3 newPositionIU = newPositionSU;
			newPositionSU.y = newPositionSU.y - 50;
			newPositionIU.x = newPositionIU.x + 50;
			newPositionIU.y = newPositionIU.y + 50;
			canvasSU.transform.position = newPositionSU;
			canvasIU.transform.position = newPositionIU;
			canvasIU.alpha = 1;
			if (mm.selectedObject != null) {
				Slot slot = mm.selectedObject.GetComponent<Slot> ();
                //Price for selling is half of the new price.
                int sellPrice = slot.unit.GetComponent<Action_Defense>().getValues().towerPrice / 2;
                String attack = slot.unit.GetComponent<Action_Defense>().getValues().strenght.ToString();
                String money = sellPrice.ToString();
                if (slot.unit != null) {
					infoUnitText.text =
						"Attack: +" +
						attack +
						"\nSell: +" +
						money;
				} 
			} else {
				Debug.Log ("Nothing Selected");
			}

		} else {
			canvasSU.alpha = 0;
			canvasSU.interactable = false;
			canvasSU.blocksRaycasts = false;
			canvasIU.alpha = 0;
		}
	}

	public void sellSelected(){
		if (mm.selectedObject != null) {
			Debug.Log ("Sell");
			Slot slot = mm.selectedObject.GetComponent<Slot> ();
			if (slot.unit != null) {
				Destroy (slot.unit);
			} 
			slot.unit = null;
			slot.isOccupied = false;

			//TODO: Money Depending on the unit
			//GameObject.FindObjectOfType<LifeAmountManager> ().GainAmount(20);
		} else {
			Debug.Log ("Nothing Selected");
		}
	}

	public void upgradeSelected(){
		//TODO
		//mm.selectedObject.transform.localScale += new Vector3(0, 1.1F, 0);


		//GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(20);
	}


		
}
