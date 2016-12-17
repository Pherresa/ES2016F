using UnityEngine;
using System.Collections;
using System;

public class BetterCamera : MonoBehaviour {
   public float mainSpeed = 100.0f;
    public float shiftAdd = 250.0f;
    public float maxShift = 1000.0f;
    public float camSens = 0.25f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun= 1.0f;
 
    private bool isRotating = false;
    private float speedMultiplier;
 
    public float mouseSensitivity     = 5.0f;
    private float rotationY            = 0.0f;

    public float maxZ = 110f, minZ = -110f;
    public float maxY = 100f, minY = 10f;
    public float maxX = 100f, minX = -110f;

    void Update () {
        if (Input.GetMouseButtonDown (1)) {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp (1)) {
            isRotating = false;
        }
        if (isRotating) {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * mouseSensitivity;        
            rotationY += Input.GetAxis ("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp (rotationY, -90, 90);        
            transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0.0f);
        }
       
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey (KeyCode.LeftShift)){
            totalRun += Time.deltaTime;
            p  = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            speedMultiplier = totalRun * shiftAdd * Time.deltaTime;
            speedMultiplier = Mathf.Clamp(speedMultiplier, -maxShift, maxShift);
        }
        else{
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
            speedMultiplier = mainSpeed * Time.deltaTime;
        }
       
        p = p * Time.deltaTime;
 
        // Locked to Y/X plane
        Vector3 newPosition = transform.position;
        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
 
        // Manipulate Y plane by using Q/E keys
        if (Input.GetKey (KeyCode.Q)){
            newPosition.y += -speedMultiplier;
        }
        if (Input.GetKey (KeyCode.E)){
            newPosition.y += speedMultiplier;
        }
 
        transform.position = newPosition;

        applyLimits();
    }

    private void applyLimits()
    {
        transform.position = new Vector3(Mathf.Min(Mathf.Max(minX, transform.position.x), maxX),
            Mathf.Min(Mathf.Max(minY, transform.position.y), maxY),
            Mathf.Min(Mathf.Max(minZ, transform.position.z), maxZ));
    }
    
    public bool amIRotating(){
        return isRotating;
    }
   
    private Vector3 GetBaseInput() {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
