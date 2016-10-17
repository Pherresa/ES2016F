using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class PlaceUnit : MonoBehaviour {


    void OnMouseUp()
    {
        Debug.Log("TowerSpot clicked.");
        ConstructionManager bm = GameObject.FindObjectOfType<ConstructionManager>();

        if (bm.selectedUnit != null)
        {
            LifeAmountManager sm = GameObject.FindObjectOfType<LifeAmountManager>();

            GameObject obj = (GameObject) Instantiate(bm.selectedUnit, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            // TODO: Depending of the type of unit...
            GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(40);

            var scriptAsset = AssetDatabase.FindAssets("SellUpgradeUnit");
            if (scriptAsset.Length > 0)
            {
                Debug.Log("script attached");
                obj.AddComponent(Type.GetType("SellUpgradeUnit"));
                SellUpgradeUnit su = (SellUpgradeUnit) obj.GetComponent(typeof(SellUpgradeUnit));
                su.selectedUnit = obj;
                su.showCanvas();
            }
            else{Debug.Log("script error");}

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

}


