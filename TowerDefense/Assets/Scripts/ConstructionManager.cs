using UnityEngine;
using System.Collections;

public class ConstructionManager : MonoBehaviour
{
    public GameObject selectedUnit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectUnit(GameObject prefab)
    {
        selectedUnit = prefab;
    }
}
