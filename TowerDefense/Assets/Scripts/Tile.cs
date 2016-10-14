using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class to wrap all the data related to a Tile
public class Tile : MonoBehaviour {

    private Coordinates position;
	private bool m_IsDestroyable;


    //Getters and setters
    public Coordinates coord
    {
        get
        {
            return coord;
        }

        set
        {
            coord = value;
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
