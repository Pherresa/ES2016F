using UnityEngine;
using System.Collections;

public class MapTerrain : Tile {

    private Tile actual_pos;
    private bool m_IsOccupied;


    //Constructor
    public MapTerrain (Tile pos, bool occupied)
    {
        actual_pos = pos;
        m_IsOccupied = occupied;
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

    public bool occupied
    {
        get
        {
            return m_IsOccupied;
        }

        set
        {
            m_IsOccupied = value;
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
