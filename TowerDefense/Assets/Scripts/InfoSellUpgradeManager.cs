using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InfoSellUpgradeManager : MonoBehaviour {

	private CanvasGroup canvasSU;
	private Text infoUnitText;
	private CanvasGroup canvasIU;
	MouseManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager> ();
		canvasSU = GameObject.Find("CanvasSU").GetComponent<CanvasGroup>();
		infoUnitText = GameObject.Find("infoUnitText").GetComponent<Text>();
		canvasIU = GameObject.Find("InfoUnit").GetComponent<CanvasGroup>();
		setActive (false);
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
				Debug.Log("INFO UNIT_______________");
				Debug.Log (mm.selectedObject.name);
				Slot slot = mm.selectedObject.GetComponent<Slot> ();
				String attack = "XXX";
				String money = "XXX";
				if (slot.unit != null) {
					if (slot.unit.GetComponent<Action_Defense> ().towerTama == 1) {
						 attack = Enemy_Values_Gene.m_little_tower("a").ToString();
						 money = Enemy_Values_Gene.m_little_tower("m").ToString();
					}
					if (slot.unit.GetComponent<Action_Defense> ().towerTama == 2) {
						 attack = Enemy_Values_Gene.m_medium_tower("a").ToString();
						 money = Enemy_Values_Gene.m_medium_tower("m").ToString();
					}
					infoUnitText.text =
						"Attack: +" +
						attack +
						"\nSell: +" +
						money;
					//TODO SHOW RANGE!
					//range = Enemy_Values_Gene.m_medium_tower("r")
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
