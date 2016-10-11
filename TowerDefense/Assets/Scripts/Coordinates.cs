using UnityEngine;
using System.Collections;
// Class to store everything related to Coordinates.
public class Coordinates
{
	private int m_pos_X;
	private int m_pos_Y;
	private int m_pos_Z;
	// Use this for initialization

	//Constructor
	public Coordinates (int x, int y, int z)
	{
		m_pos_X = x;
		m_pos_Y = y;
		m_pos_Z = z;
	}

	//Getters and setters.
	public int pos_X
	{
		get 
		{ 
			return m_pos_X; 
		}
		
		set
		{
			m_pos_X = value;
		}
	}

	public int pos_Y
	{
		get 
		{ 
			return m_pos_Y; 
		}

		set
		{
			m_pos_Y = value;
		}
	}

	public int pos_Z
	{
		get 
		{ 
			return m_pos_Z; 
		}

		set
		{
			m_pos_Z = value;
		}
	}


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
