using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeneralEnemy : MonoBehaviour {

    // aquesta variable no caldria. Si l'bjcte esta creat es que esta viu.
	public bool alive;
	public int maxLife;
	public int life;
	public int damage;
	public int timeAttack;

	public Text EnemyLifeText;

	// Use this for initialization
	void Start () {
		//For example
		maxLife = 10;
		life = 10;

		// Todo
		damage = 0;
		timeAttack = 0;
		alive = true;
		EnemyLifeText = GameObject.Find("EnemyLifeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		


		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 w = new Vector3(pos.x, pos.y, 0.0f);
		EnemyLifeText.transform.position = w;

		

		if(alive){
			EnemyLifeText.text = life.ToString()+"/"+maxLife.ToString();
		}
		else{
			EnemyLifeText.text = "  ";
		}
		
	}

    // En comptes de no renderitzarlo, s'hauria de destuir.
	public void downLife(int damage){
		life = life-damage;
		if (life<=0){
			alive= false;
			//Animation to make it disappear
			
			GetComponent<MeshRenderer>().enabled = false;
			//Destroy(this);

		}

	}


}
