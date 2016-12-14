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
            GameManager sm = GameObject.FindObjectOfType<GameManager>();

            GameObject obj = (GameObject) Instantiate(bm.selectedUnit, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            // TODO: Depending of the type of unit...
            GameObject.FindObjectOfType<GameManager>().LoseAmount(40);

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


