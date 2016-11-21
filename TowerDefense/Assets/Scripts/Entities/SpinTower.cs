using UnityEngine;
using System.Collections;

/*
 * Class that spin a tower
 */ 
public class SpinTower : MonoBehaviour
{ 
		 

	// Tower spenning: if detects an enemy in the radius of the tower
	public static void spin(Vector3 pos_enemy, Transform towerTransform) { 
		// speed how the tower make spinning
		float turnSpeed = 1f; 
		// direction of forward vector
		Vector3 dir; 
		// we obtain it first
		dir = pos_enemy - towerTransform.position; 
		// normalize the forward vector
		dir.Normalize(); 
		// we change it because the model trebuchet hasn't the forward vector correctly
		dir = Quaternion.Euler (0f, 80f, 0f) * dir; 
		// now we rotate the tower in the direction of the nearest enemy
		towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
 
	} 

 

}
