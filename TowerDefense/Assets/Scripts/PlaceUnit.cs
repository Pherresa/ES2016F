using UnityEngine;
using System.Collections;
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

    // Un problema que veiem es que cada PlaceUnit pardadeja a un ritme diferent. Es pot solucionar tenint mes en compte el deltatime, per millorar el parpadeig.
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
    // es millor anomenarla setter. ja que les funcions que comencen amb 'is' solen retornar booleans.
    public void isClicked(bool b){
        clicked = b;
    }





     // les variables de  free es una variable per saber si hi ha un objecte, i la de iscliked(false) es un setter per posar si
     // esta clicat o no. Es podria pensar de fe-ho amb una variable generica per a tots el placeunits.

     // En el cas de vendre, els place units no parpadejen com a lliures. S'ha de controlar aquest aspecte.
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
            // TODO: Depending of the type of unit...
            GameObject.FindObjectOfType<LifeAmountManager>().LoseAmount(40);

            obj.AddComponent(Type.GetType("SellUpgradeUnit"));
            SellUpgradeUnit su = (SellUpgradeUnit) obj.GetComponent(typeof(SellUpgradeUnit));
            su.selectedUnit = obj;
            su.showCanvas();
            GameObject.FindObjectOfType<UnitInfo>().hideInfo();

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


