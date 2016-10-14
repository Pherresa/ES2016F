using UnityEngine;
using System.Collections;

//Class to save the data of the path in the map
public class Path : Tile {

    private Tile direction;
    private double m_speed;

    //Constructor
    public Path (Tile tile, double spd)
    {
        direction = tile;
        m_speed = spd;
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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
