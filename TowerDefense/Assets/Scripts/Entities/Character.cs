using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	// We create a new variable type
	public enum TypeKingdom 
	{
		NONE = 0,
		GONDOR = 1,
		ROHAN = 2,
		MORDOR = 3,
		 
	}

	// We define a static string
	public static string tagOfEnemy = "Enemy";

	// Atributes of a character
	private string name; 	// Name of the character
	private TypeKingdom kingdom; // kingdom where the character belongs
	private int age; 		// Age of the character
	private int life;		// Life of the character
	private int strenght; 	// Physical strength of character)
	private int agility;  	// Agility Character: pirouettes, somersaults...
	private int gunnery;  	// Ability to hit a target at a distance
	private int reflection; // Ability to react quickly to a physical impulse
	private int speed; 		// How fast moves the character

	 
	// Constructors of this class
	public Character(): 
	this("##",TypeKingdom.NONE,0,0,0,0,0,0,0) {}

	public Character(string Cname): 
	this(Cname,TypeKingdom.NONE,0,0,0,0,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom): 
		this(Cname,Ckingdom,0,0,0,0,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage): 
		this(Cname,Ckingdom,Cage,0,0,0,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife): 
		this(Cname,Ckingdom,Cage,Clife,0,0,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife, int Cstrenght): 
		this(Cname,Ckingdom,Cage,Clife, Cstrenght,0,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife, int Cstrenght, int Cagility): 
		this(Cname,Ckingdom,Cage,Clife,Cstrenght,Cagility,0,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife, int Cstrenght, int Cagility, int Cgunnery): 
		this(Cname,Ckingdom,Cage,Clife,Cstrenght,Cagility,Cgunnery,0,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife, int Cstrenght, int Cagility, int Cgunnery, int Creflection): 
		this(Cname,Ckingdom,Cage,Clife,Cstrenght,Cagility,Cgunnery,Creflection,0) {}

	public Character(string Cname, TypeKingdom Ckingdom, int Cage, int Clife, int Cstrenght, int Cagility, int Cgunnery, int Creflection, int Cspeed) 
	{
		this.name = Cname;
		this.kingdom = Ckingdom;
		this.age = Cage;
		this.life = Clife;
		this.strenght = Cstrenght;
		this.agility = Cagility;
		this.gunnery = Cgunnery;
		this.reflection = Creflection;
		this.speed = Cspeed;
	}



	// Setters and getters of this class
	public string Name 
	{
		get{return name;}
		set{name = value;}
	}

	public TypeKingdom Kingdom 
	{
		get{return kingdom;}
		set{kingdom = value;}
	}

	public int Age 
	{
		get{return age;}
		set{age = value;}
	}

	public int Life 
	{
		get{return life;}
		set{life = value;}
	}

	public int Strenght 
	{
		get{return strenght;}
		set{strenght = value;}
	}

	public int Agility 
	{
		get{return agility;}
		set{agility = value;}
	}

	public int Gunnery 
	{
		get{return gunnery;}
		set{gunnery = value;}
	}

	public int Reflection
	{
		get{return reflection;}
		set{reflection = value;}
	}

	public string Speed 
	{
		get{return speed;}
		set{speed = value;} 
	}

 



	// Get the nearest enemy if it exist
	public GameObject getTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagOfEnemy);
		float tmpDistance = Mathf.Infinity;
		GameObject tmpEnemy = null, target = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < tmpDistance)
			{
				tmpDistance = distanceToEnemy;
				tmpEnemy = enemy;
			}
		}
		if (tmpEnemy != null && tmpDistance <= Gunnery)
		{
			target = tmpEnemy;
		}
		return target;
	}



	// Move the character depending by his speed
	public void moveCharcter()
	{
		float moveHoritzontal = Input.GetAxis ("Horitzontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHoritzontal, 0.0f, moveVertical);

		rigidbody.AddForce (movement * speed * Time.deltaTime);

	}


	// The character die
	public void die() 
	{
		this.life = 0;
	}


	// Prints in the terminl window
	public override string ToString ()
	{
		return string.Format ("Hi, my name is {0} belonging to {1} kingdom, I'm {2} years old.\n " +
			"With life={3}, strenght={4}, agility={5}, gunnery={6}, reflection={7} and speed={8}",
			Name,Kingdom,Age,Life,Strenght,Agility,Gunnery,Reflection,Speed);
	}
 



}
