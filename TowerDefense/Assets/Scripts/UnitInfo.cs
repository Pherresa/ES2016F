using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    public GameObject selectedUnit;
    private Text infoUnitText;

    // Use this for initialization
    void Start()
    {
        infoUnitText = GameObject.Find("infoUnitText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showInfo(GameObject prefab)
    {
        selectedUnit = prefab;
        infoUnitText.text = "Power: " +
            selectedUnit.name.Substring(selectedUnit.name.Length - 3, 3) +
            "\nCost: " +
            selectedUnit.name.Substring(selectedUnit.name.Length - 3, 3) + "000";

    }
}
