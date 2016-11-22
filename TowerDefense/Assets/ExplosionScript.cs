using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour
{
    ParticleSystem[] systems;
    bool particleSystemsDone = false, started = false;

    // Use this for initialization
    void Start()
    {
        systems = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        particleSystemsDone = false;
        foreach (ParticleSystem system in systems)
        {
            particleSystemsDone = system.IsAlive();

            if (particleSystemsDone) break;
        }
        if (!particleSystemsDone) Destroy(gameObject);
    }
}
