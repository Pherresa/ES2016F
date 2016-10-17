using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class PlaceUnit : MonoBehaviour {

    public float timer;
    public float WaitTime;
    public float ResetPoint;

    public Renderer rend;
    public bool free;
    public bool clicked;

    public Color origColor;


    void Start(){
        // We get renderer
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        // Get original color
        origColor = rend.material.color;
        free = true;
        clicked = false;
        WaitTime = 0.5f;
        ResetPoint = WaitTime * 2;

    }


    void Update(){
        // If one button is clicked, placeUnit blinks
        if (clicked){
            timer += Time.deltaTime;
     
            if(timer < WaitTime){
            rend.material.color = origColor;
            }

            if(timer > WaitTime){
            rend.material.color = Color.green;
            }

            if(timer > ResetPoint){
            timer = 0;
            }
        }
        else{
            rend.material.color = origColor;
        }
    }

    public void isClicked(bool b){
        clicked = b;
    }






    void OnMouseUp()
    {
        Debug.Log("TowerSpot clicked.");
        ConstructionManager bm = GameObject.FindObjectOfType<ConstructionManager>();

        if (bm.selectedUnit != null)
        {
            free = false;
            isClicked(false);
            GameObject.FindObjectOfType<ConstructionManager>().stopBlinking();

            LifeAmountManager sm = GameObject.FindObjectOfType<LifeAmountManager>();

            GameObject obj = (GameObject) Instantiate(bm.selectedUnit, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            
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


