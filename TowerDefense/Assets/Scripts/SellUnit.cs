using UnityEngine;
using System.Collections;
using UnityEditor;
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

        var scriptAsset = AssetDatabase.FindAssets("PlaceUnit");
        if (scriptAsset.Length > 0)
        {
            newObj.AddComponent(Type.GetType("PlaceUnit"));

        }
        else { Debug.Log("script error"); }

        Destroy(selectedUnit);
        selectedUnit = null;
        //TO DO: Money back?
    }
}
