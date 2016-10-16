using UnityEngine;
using System.Collections;

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


    public void UpgradeSelected()
    {
        Debug.Log("Upgrade Selected");

        selectedUnit.transform.localScale += new Vector3(0, 1.1F, 0);

        //TO DO: Money back?
    }
}
