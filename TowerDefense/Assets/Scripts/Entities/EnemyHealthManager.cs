using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    private Enemy enemy;
    private RectTransform healthRectTransform;

    // Use this for initialization
    void Start () {
        //get the thing component on your instantiated object
        enemy = GetComponentInParent<Enemy>();
        healthRectTransform = (RectTransform)gameObject.transform.Find("Health").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        if(enemy.life < enemy.maxlife)
        {
            HandleHealthChangedAction(enemy.life, enemy.maxlife);
        }
    }

    void HandleHealthChangedAction(float currentHealth, float maxHealth)
    {
        healthRectTransform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
}
