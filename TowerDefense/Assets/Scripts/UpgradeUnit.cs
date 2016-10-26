using UnityEngine;
using System.Collections;

/*
 * Upgrade class control the upgrade of the unit.
 */

public class UpgradeUnit : MonoBehaviour {

    public GameObject selectedUnit = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	//UpgradeSelected method upgrade the last unit which has been selected.

    public void UpgradeSelected()
    {
        Debug.Log("Upgrade Selected");
		//TO DO
        selectedUnit.transform.localScale += new Vector3(0, 1.1F, 0);

        //TO DO: Money?
        GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(20);
    }
}
