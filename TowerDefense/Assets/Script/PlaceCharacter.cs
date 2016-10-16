using UnityEngine;
using System.Collections;

public class PlaceCharacter : MonoBehaviour {

	public Transform[] points;
	private int currentPoint;
	public float moveSpeed;



	// Use this for initialization
	void Start () {

		transform.position = points [0].position;
		currentPoint = 0;

	}


	void Update(){

		if (transform.position == points [currentPoint].position) {
			currentPoint++;
		}
		//start again
		if (currentPoint >= points.Length) {
			currentPoint = 0;
		}
		transform.position = Vector3.MoveTowards (transform.position, points [currentPoint].position, moveSpeed * Time.deltaTime);
	}
}


