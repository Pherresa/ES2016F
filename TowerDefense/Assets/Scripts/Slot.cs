using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

	public bool isPath;
	public bool isOccupied;
	public GameObject unit;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
		isOccupied = false;
		unit = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getIsPath(){
		return isPath;
	}


	public void SetActive(bool active){
		gameObject.SetActive(active);
	}

    void OnMouseDown() {
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.AddListener(sell_obj);
        
    }

    private void sell_obj() {
        GameObject.Find("GameManager").GetComponent<LifeAmountManager>().GainAmount(unit.GetComponent<Action_Defense>().towerPrice);
        isOccupied = false;
        Destroy(unit);
    }
}
