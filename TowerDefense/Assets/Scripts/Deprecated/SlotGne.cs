using UnityEngine;
using System.Collections;
using Pathfinding;

public class SlotGne : MonoBehaviour {
    GameObject slotPrefab;

    // Use this for initialization
    void Start () {

        AstarPath aStar = GameObject.Find("A*").GetComponent<AstarPath>();
        slotPrefab = Resources.Load("Prefabs/Slot") as GameObject;

        for (int i = -100; i < 100; i+=10)
        {
            for(int j = -100; j < 100; j+=10)
            {
                Vector3 position = new Vector3((float)i, 2.0f, (float)j);
                
                GameObject slot = (GameObject)Instantiate(slotPrefab, position, slotPrefab.transform.rotation, transform);
                slot.name = slot.name + i + j + Random.Range(i, j);

                NNConstraint constrainWalkable = new NNConstraint();
                constrainWalkable.constrainArea = false;
                constrainWalkable.constrainTags = false;
                constrainWalkable.constrainWalkability = true;
                constrainWalkable.walkable = true;

                NNConstraint constrainNonWalkable = new NNConstraint();
                constrainNonWalkable.constrainArea = false;
                constrainNonWalkable.constrainTags = false;
                constrainNonWalkable.constrainWalkability = true;
                constrainNonWalkable.walkable = false;

                GraphNode walkableNode = aStar.GetNearest(position, constrainWalkable).node;
                GraphNode nonWalkableNode = aStar.GetNearest(position, constrainNonWalkable).node;

                float walkableDistance = Vector3.Distance(slot.transform.position, (Vector3)walkableNode.position);
                float nonWalkableDistance = Vector3.Distance(slot.transform.position, (Vector3)nonWalkableNode.position);

                if (walkableDistance < nonWalkableDistance)
                {
                    slot.GetComponent<Slot>().isPath = true;
                }

            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
