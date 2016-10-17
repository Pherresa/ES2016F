using UnityEngine;
using System.Collections;

public class ConstructionManager : MonoBehaviour
{
    public GameObject selectedUnit;
    private PlaceUnit[] placeUnits;
    private Color[] origColors;
    private Color colorPlaceUnit;

    // Use this for initialization
    void Start()
    {
        // Get all placeUnits
        placeUnits = FindObjectsOfType(typeof(PlaceUnit)) as PlaceUnit[]; 

        // In future, get color or material of each placeUnit

        /*int i =0;
        int n = sizeof(placeUnits) / sizeof(placeUnits[0]);
        origColors = new Color[n];
        foreach (var pu in placeUnits){
            Renderer r = pu.rend;
            origColors[i] = r.material.color;
            i = i+1;    
        }
        */

        // For now, get color of one placeUnit (suppose all same color)
        colorPlaceUnit = placeUnits[0].rend.material.color;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectUnit(GameObject prefab)
    {
        // For each placeUnit
        foreach (var pu in placeUnits){
            // If placeUnit is free
            if (pu.free == true){
                // Tell that button is clicked, so free placeUnits can show they are available
                pu.isClicked(true);
            }
        }


        selectedUnit = prefab;
    }

    public void stopBlinking(){
        int i = 0;
        foreach (var pu in placeUnits){

            if (pu.free == true){
                pu.isClicked(false);
                Renderer r = pu.rend;
                // Future
                //r.material.color = origColors[i];
                r.material.color = colorPlaceUnit;

            }
            i=i+1;
        }
    }



}