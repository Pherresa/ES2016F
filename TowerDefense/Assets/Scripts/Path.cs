using UnityEngine;
using System.Collections;

//Class to save the data of the path in the map
public class Path : Tile {

    private Tile direction;
    private Tile actual_pos;
    private double m_speed;

    //Constructor
    public Path (Tile tile, Tile pos, double spd)
    {
        direction = tile;
        actual_pos = pos;
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

    public double speed
    {
        get
        {
            return m_speed;
        }

        set
        {
            m_speed = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
