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
            

        
            Instantiate(bm.selectedUnit, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

}


