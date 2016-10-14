using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class to wrap all the data related to a Tile
public class Tile : MonoBehaviour {

	private Coordinates position = GetComponent<Coordinates>();
	private bool m_IsDestroyable;


	public Tile (int x, int z, int y, bool destroyable)
	{
		position = new Coordinates(x,y,z);
		m_IsDestroyable = destroyable;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
