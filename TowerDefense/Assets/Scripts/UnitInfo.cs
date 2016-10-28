using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    public GameObject selectedUnit;
    private Text infoUnitText;
    private CanvasGroup canvasIU;

    // Use this for initialization
    void Start()
    {
        infoUnitText = GameObject.Find("infoUnitText").GetComponent<Text>();
        canvasIU = GameObject.Find("InfoUnit").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideInfo()
    {
        canvasIU.alpha = 0;
    }

    public void showInfo(GameObject prefab)
    {

        canvasIU.alpha = 1;
        selectedUnit = prefab;
        infoUnitText.text = "Power: " +
            selectedUnit.name.Substring(selectedUnit.name.Length - 3, 3) +
            "\nCost: " +
            selectedUnit.name.Substring(selectedUnit.name.Length - 3, 3) + "000";

    }
}
