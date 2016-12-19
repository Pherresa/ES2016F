
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InfoSellUpgradeManager : MonoBehaviour {

	private CanvasGroup canvasSU;
	private Text infoUnitText;
	private CanvasGroup canvasIU;
	private CanvasGroup HUDCanvas;
	private int sellPrice;
	GameObject auraPrefab;
	GameObject selectedObject;
	MouseManager mm;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager> ();
		canvasSU = GameObject.Find("CanvasSU").GetComponent<CanvasGroup>();
		infoUnitText = GameObject.Find("infoUnitText").GetComponent<Text>();
		canvasIU = GameObject.Find("InfoUnit").GetComponent<CanvasGroup>();
		setActive (false);
		sellPrice = 0;
		GameObject.Find("ButtonSell").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("ButtonSell").GetComponent<Button>().onClick.AddListener(sellSelected);
		auraPrefab = Resources.Load("Prefabs/AreaProjector") as GameObject;
	}

	// Update is called once per frame
	void Update () {
		if (mm.selectedObject != null){
			setActive (true);
			selectedObject = mm.selectedObject;
		} else {
			setActive (false);
		}
	}

	void setActive(bool en){
		if (en) {
			
			if (mm.selectedObject != null) {
				//Slot slot = mm.selectedObject.GetComponent<Slot> ();
				//Price for selling is half of the new price.
				if(mm.selectedObject.name.StartsWith("Slot")){
					return;
				}
				canvasSU.alpha = 1;
				canvasSU.interactable = true;
				canvasSU.blocksRaycasts = true;
				canvasIU.alpha = 1;
				canvasIU.interactable = true;
				canvasIU.blocksRaycasts = true;

				Vector3 newPositionSU = Camera.main.WorldToScreenPoint (mm.selectedObject.transform.position);
				Vector3 newPositionIU = newPositionSU;
				newPositionSU.y = newPositionSU.y - 50;
				newPositionIU.x = newPositionIU.x + 50;
				newPositionIU.y = newPositionIU.y + 50;
				canvasSU.transform.position = newPositionSU;
				canvasIU.transform.position = newPositionIU;
				canvasIU.alpha = 1;
				Action_Defense unit = mm.selectedObject.GetComponent<Action_Defense>();
				sellPrice = unit.getValues().towerPrice / 2;
				String attack = unit.getValues().strenght.ToString();
				String money = sellPrice.ToString();
				if (unit != null) {
					infoUnitText.text =
						"Attack: +" +
						attack +
						"\nSell: +" +
						money;
				} 

				//GameObject aura = Instantiate(auraPrefab);
				//aura.GetComponent<Projector>().orthographicSize = unit.getValues().range;//prefabActionDefense.range; 
				//aura.GetComponent<Projector>().enabled = true;
				//aura.transform.position = unit.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
				//aura.transform.parent = unit.transform;

			} else {
				Debug.Log ("Nothing Selected IN SET ACTIVE");
			}

		} else {
			canvasSU.alpha = 0;
			canvasSU.interactable = false;
			canvasSU.blocksRaycasts = false;
			canvasIU.alpha = 0;
			//canvasIU.interactable = false;
			//canvasIU.blocksRaycasts = false;
		}
	}

	public void sellSelected(){
		Debug.Log("Selling");
		if (selectedObject != null) {
			Debug.Log ("Sell");
			Destroy (selectedObject);
			selectedObject = null;
			//TODO Free Slot
			//slot.isOccupied = false;
			GameObject.FindObjectOfType<GameManager>().GainAmount(sellPrice);
		}
	}

	public void upgradeSelected(){
		//TODO
		//mm.selectedObject.transform.localScale += new Vector3(0, 1.1F, 0);


		//GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(20);
	}



}



