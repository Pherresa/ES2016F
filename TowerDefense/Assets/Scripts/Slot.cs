using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

	public bool isPath;
	public bool isOccupied;
	public bool isWater;
	public bool isTowerSlot;
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


	public bool getIsTowerSlot(){
		return isTowerSlot;
	}

	public bool getIsPath(){
		return isPath;
	}

	public bool getIsWater(){
		return isWater;
	}

	public void SetActive(bool active){
		gameObject.SetActive(active);
	}

    void OnMouseDown() {
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonSell").GetComponent<Button>().onClick.AddListener(sell_obj);
        
    }

    private void sell_obj() {
        GameObject.Find("GameManager").GetComponent<GameManager>().GainAmount(unit.GetComponent<Action_Defense>().getValues().towerPrice / 2);
        isOccupied = false;
        Destroy(unit);

    }

    void OnTriggerEnter(Collider other)
    {
    }
}
