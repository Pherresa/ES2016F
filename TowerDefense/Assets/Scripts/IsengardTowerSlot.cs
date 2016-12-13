using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IsengardTowerSlot : Slot {

	public bool isSaruman;
	public bool isHurukhai;
	public GameObject unit;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
	    unit = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive(bool active){
		gameObject.SetActive(active);
	}

    void OnMouseDown() {
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.AddListener(sell_obj);
        
    }

    private void sell_obj() {
        GameObject.Find("GameManager").GetComponent<GameManager>().GainAmount(unit.GetComponent<Action_Defense>().getValues().towerPrice);
        Destroy(unit);
    }

  
}
