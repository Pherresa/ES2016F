using UnityEngine;
using System.Collections;

//Class to save the data of the path in the map
public class Route : Tile {

    private Tile direction;
    private Tile actual_pos;

    //Constructor
    public Route(Tile tile, Tile pos)
    {
        direction = tile;
        actual_pos = pos;
    }

    //Getters and setters
    public Tile dir
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public Tile pos
    {
        get
        {
            return actual_pos;
        }

        set
        {
            actual_pos = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
