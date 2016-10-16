using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class Initialize : MonoBehaviour
{

    static Initialize()
    {
        Debug.Log("Init");
        
    }

    void Start()
    {
        setSellUpgradeInActive();
    }

    static void setSellUpgradeInActive()
    {
        Debug.Log("Inavtive");
        GameObject canvasSU = GameObject.Find("CanvasSU");
        canvasSU.GetComponent<CanvasGroup>().alpha = 0;
        canvasSU.GetComponent<CanvasGroup>().interactable = false;
        canvasSU.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //canvasSU.SetActive(false);
        Debug.Log(canvasSU.activeSelf);
    }

}
