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
			newPositionIU.x = newPositionIU.x + 80;
			canvasSU.transform.position = newPositionSU;
			canvasIU.transform.position = newPositionIU;
			canvasIU.alpha = 1;
			//TO DO: change power and cost depending on unit
			infoUnitText.text = "Power: " +
				"XXXX" +
				"\nCost: +" +
				"20";

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
			GameObject empty = (GameObject) Resources.Load("prefabs/Empty", typeof(GameObject));
			GameObject newObj = (GameObject) Instantiate(empty, mm.selectedObject.transform.position - new Vector3(0, 0.5f, 0), mm.selectedObject.transform.rotation);

			newObj.AddComponent(Type.GetType("PlaceUnit"));

			Destroy(mm.selectedObject);
			mm.selectedObject = null;

			//TO DO: Money Depending on the unit
			GameObject.FindObjectOfType<LifeAmountManager> ().GainAmount(20);
		} else {
			Debug.Log ("Nothing Selected");
		}
	}

	public void upgradeSelected(){
		mm.selectedObject.transform.localScale += new Vector3(0, 1.1F, 0);

		//TO DO: Money Depending on the unit
		GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(20);
	}


		
}
