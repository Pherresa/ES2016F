using UnityEngine;
using System.Collections;

public class MiniTower : Tower {

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
        iniStates();
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();
        if (target == null)
            return;
        print(target);
    }

    void iniStates()
    {
        range = 10f;
        life = 50;
        strenght = 3;
    }


    protected override void Shoot()
    {
        // TODO
    }

    protected override void Destroy()
    {
        // TODO
    }
}
