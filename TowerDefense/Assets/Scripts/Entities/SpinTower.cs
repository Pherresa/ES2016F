using UnityEngine;
using System.Collections;

/*
 * Clase que va a utilizar el proyectil
 */ 
public class SpinTower : MonoBehaviour
{

	private Transform myTransform;
	public Vector3 pos; 
	private Transform towerTransform;

	void Start()
	{
		//pos = target.transform.position; 
		towerTransform = this.transform; 
	}
		

	void Update() {  
		Enemy[] enemies = (Enemy[]) FindObjectsOfType<Enemy>();
		float minDist = 999999;
		Enemy nearestEnemy = null;
		for (int i = 0; i < enemies.Length; i++) {
			float dist = Vector3.Distance (enemies [i].gameObject.transform.position, this.transform.position);
			if (dist < minDist) {
				minDist = dist;
				nearestEnemy = enemies[i];
			}
			Debug.Log (enemies [i].gameObject.transform.position);
		}
		spinTower ( nearestEnemy.gameObject.transform.position ); // Spin the tower.
	}

	// Tower spenning: if detects an enemy in the radius of the tower
	void spinTower(Vector3 pos_enemy) {
		//code review

		//
		float turnSpeed = 1f;
		Vector3 dir;
		/*
		Debug.Log("hola1"+pos_enemy);
		Debug.Log(towerTransform.rotation.eulerAngles.x+" "+towerTransform.rotation.eulerAngles.y+" "+towerTransform.rotation.eulerAngles.z);
		towerTransform.LookAt (pos_enemy);
		Debug.Log("hola2"+pos_enemy);
		Debug.Log(towerTransform.rotation.eulerAngles.x+" "+towerTransform.rotation.eulerAngles.y+" "+towerTransform.rotation.eulerAngles.z);
		towerTransform.rotation = Quaternion.LookRotation(pos_enemy - towerTransform.position);
		Debug.Log("hola3"+pos_enemy);
		Debug.Log(towerTransform.rotation.eulerAngles.x+" "+towerTransform.rotation.eulerAngles.y+" "+towerTransform.rotation.eulerAngles.z);
	
		// Spinning tower towards enemy.
		Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(pos_enemy - towerTransform.position), turnSpeed * Time.deltaTime);
		*/
		//code review
		dir = pos_enemy - this.transform.position;
		dir.Normalize();
		dir = Quaternion.Euler (0f, 90f, 0f) * dir;
		this.transform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
		//cr

		/*if(towerTransform){
			dir = pos_enemy - towerTransform.position;
			dir.Normalize();
			//towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
			towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), 0.5f);

		}*/
	} 




}
