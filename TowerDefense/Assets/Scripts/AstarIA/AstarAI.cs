using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour {

	public Transform target;

	private Seeker seeker;

	public float speed;

	float nextWaypointDistance = 2f;

	CharacterController characterController;

	//The calculated path
	public Path path;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();
		characterController = GetComponent<CharacterController>();

		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,target.position, OnPathComplete);
	}

	public void OnPathComplete (Path p) {
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		} else {
			Debug.Log (p.error);
		}
	}

	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			return;
		}

		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized * speed;
		characterController.SimpleMove (dir);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public float Speed { get; set; }
}
