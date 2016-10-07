using UnityEngine;
using System.Collections;

public class PlaceUnit : MonoBehaviour {


    void OnMouseUp()
    {
        Debug.Log("TowerSpot clicked.");
        ConstructionManager bm = GameObject.FindObjectOfType<ConstructionManager>();
        if (bm.selectedUnit != null)
        
        {
            LifeAmountManager sm = GameObject.FindObjectOfType<LifeAmountManager>();
            

        
            Instantiate(bm.selectedUnit, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

}


