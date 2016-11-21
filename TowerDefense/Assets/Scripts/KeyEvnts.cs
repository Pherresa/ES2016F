using UnityEngine;
using System.Collections;

public class KeyEvnts : MonoBehaviour {

    public GameObject escMenu;
    public GameObject mainCamera;
    public float speed = 1.0f;
    private bool escMenuIsActive = false;

    public float maxZ = 110f, minZ = -110f;
    public float maxY = 80f, minY = 0f;
    public float maxX = 100f, minX = -110f; 

	// Use this for initialization
	void Start () {
        escMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenuIsActive)
            {
                Time.timeScale = 1.0f;
                escMenu.SetActive(false);
                escMenuIsActive = false;
            }
            else
            {
                escMenu.SetActive(true);
                Time.timeScale = 0.0f;
                escMenuIsActive = true;

            }
        }

        //Instanciate towers
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        // Movement camera
        else if (Input.GetKey(KeyCode.Q))
        {
            mainCamera.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            mainCamera.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.W))
        {
           mainCamera.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        //Rotation camera
        else if (Input.GetKey(KeyCode.J))
        {
            mainCamera.transform.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.L))
        {
            mainCamera.transform.Rotate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.I))
        {
            mainCamera.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.K))
        {
            mainCamera.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
        }


        limitsCamera();

    }

    void limitsCamera()
    {
        mainCamera.transform.position = new Vector3(Mathf.Min(Mathf.Max(minX, mainCamera.transform.position.x), maxX),
            Mathf.Min(Mathf.Max(minY, mainCamera.transform.position.y), maxY),
            Mathf.Min(Mathf.Max(minZ, mainCamera.transform.position.z), maxZ));
    }

}
