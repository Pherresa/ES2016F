using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour {

	public Transform target;

	private Seeker seeker;

	public float speed;
    public float turnSpeed = 10f;
    public Transform enemyBody;
    public Transform enemyCompass;

	float nextWaypointDistance = 2f;

	CharacterController characterController;

	//The calculated path
	public Path path;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;
    private GameManager lifeAmountManager;
    private Enemy enemy;

    // Use this for initialization
    void Start () {
		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();
		characterController = GetComponent<CharacterController>();
        lifeAmountManager = GameObject.FindObjectOfType<GameManager>();
        enemy = GetComponentInParent<Enemy>();

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

	public void Update () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count-5) {
			Debug.Log ("End Of Path Reached");
            checkPosition();
            return;
		}

		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized * speed;
		characterController.SimpleMove (dir);
        enemyCompass.LookAt(path.vectorPath[currentWaypoint]);
        enemyBody.rotation = Quaternion.Lerp(enemyBody.rotation, enemyCompass.rotation, Time.deltaTime * turnSpeed);

        if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
        

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    private void checkPosition()
    {
        lifeAmountManager.LoseLife(enemy.damage);
        Destroy(gameObject);
    }

    public float Speed { get; set; }
}
