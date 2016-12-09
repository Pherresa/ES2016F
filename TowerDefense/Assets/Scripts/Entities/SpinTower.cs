using UnityEngine;

/*
 * Class that spin a tower
 */ 
public class SpinTower : MonoBehaviour
{ 
	// Tower spenning: if detects an enemy in the radius of the tower
	public static void spin(Vector3 enemyPosition, Transform towerTransform) { 
		float turnSpeed = 4f;
        enemyPosition.y = 0;

        // Fix forward vector
        Quaternion newRotation = Quaternion.LookRotation(towerTransform.position - enemyPosition, Vector3.forward) * Quaternion.Euler(0f, -90f, 0f); 
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;
        towerTransform.rotation = Quaternion.Slerp(towerTransform.rotation, newRotation, Time.deltaTime * turnSpeed);
    }
}
