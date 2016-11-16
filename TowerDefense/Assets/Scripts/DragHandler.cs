using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    GameObject hoverPrefab;
    public GameObject[] availableSlots;
    //GameObject activeSlot;

    Slot activeSlot;
    public Slot[] Slots;


    /**
     * Prefab Unit instantation still not active, ready to be drag 
     * */
    void Start()
    {
    	Slots = FindObjectsOfType(typeof(Slot)) as Slot[]; 
        hoverPrefab = Instantiate (prefab);
        AdjustPrefabAlpha();
		hoverPrefab.SetActive (false);
    }


    /**
     * Color change for hoverPrefab 
     * */
    void AdjustPrefabAlpha()
    {
        foreach(MeshRenderer meshRenderer in hoverPrefab.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.25f));
        };
    }

    /**
     * Method called every time the object is dragged 
     */
    public void OnDrag(PointerEventData eventData)
    {
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 5000f);
        if (hits != null && hits.Length > 0)
        {
            MaybeShowHoverPrefab(hits);
            
            int slotIndex = GetSlotIndex(hits);
            if (slotIndex != -1)
            {
                GameObject slotQuadObject = hits[slotIndex].collider.gameObject;
                Slot slotQuad = slotQuadObject.GetComponent<Slot>();
                activeSlot = slotQuad;
                EnableSlot(slotQuad);
            }
            else
            {
                activeSlot = null;
                DisableAllSlots();
            }
        }
    }

    void EnableSlot(Slot slot)
    {
        foreach (Slot availableSlot in Slots)
        {
            if (slot.name.Equals(availableSlot.name))
            {
				if(availableSlot.getIsPath() || availableSlot.isOccupied){
					availableSlot.GetComponent<MeshRenderer> ().enabled = true;
					availableSlot.GetComponent<Renderer> ().material.color = Color.red;
				}
				else{
					availableSlot.GetComponent<MeshRenderer> ().enabled = true;
					availableSlot.GetComponent<Renderer> ().material.color = Color.green;
				}

            }
            else
            {
                availableSlot.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void DisableAllSlots()
    {
        foreach (Slot availableSlot in Slots)
        {
            availableSlot.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /**
     * Placing the object in the terrain position
     */
    void MaybeShowHoverPrefab(RaycastHit[] hits)
    {
        int terrainCollderQuadIndex = GetTerrainColliderQuadIndex(hits);
        if (terrainCollderQuadIndex != -1)
        {
            hoverPrefab.transform.position = hits[terrainCollderQuadIndex].point;
            hoverPrefab.SetActive(true);
        }
        else
        {
            hoverPrefab.SetActive(false);
        }
    }

    /**
     * Looking for Terrain hit 
     * Returning the corresponding index into the hits array
     * */
    int GetTerrainColliderQuadIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.Equals("Terrain"))
            {
                return i;
            }
        }

        return -1;
    }

    // Returns an index on a slot if the mouse is hovering over it.
    int GetSlotIndex(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.StartsWith("Slot"))
            {
                return i;
            }
        }
        return -1;
    }

    /**
     * Instantiating prefab into de Quad, setting the slot inactive, deactivating hoverPrefab
     * */
    public void OnEndDrag(PointerEventData eventData)
    {
        if (activeSlot != null)
        {
			// MeshFilter mf = activeSlot.GetComponent<MeshFilter> ();
			if(!activeSlot.getIsPath() && !activeSlot.isOccupied){
				Vector3 quadCentre = GetQuadCentre (activeSlot.gameObject);
				GameObject newUnit = (GameObject) Instantiate (prefab, quadCentre, Quaternion.identity);
                //activeSlot.SetActive (false);
                newUnit.GetComponent<Action_Defense>().activate();
                foreach(ParticleSystem particleSystem in newUnit.GetComponentsInChildren<ParticleSystem>())
                {
                    particleSystem.Play();
                }

				activeSlot.isOccupied = true;
				activeSlot.unit = newUnit;
				activeSlot.GetComponent<MeshRenderer> ().enabled = false;
			}
			else{
				activeSlot.SetActive(false);
			}
        }
        else
        {
            Destroy(hoverPrefab);
        }

        // Then set it to inactive ready for the next drag!
        hoverPrefab.SetActive (false);
    }

    /**
     * Quad center for prefab position instance
     * */
    Vector3 GetQuadCentre(GameObject quad)
    {
        Vector3[] meshVerts = quad.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] vertRealWorldPositions = new Vector3[meshVerts.Length];

        for (int i = 0; i < meshVerts.Length; i++)
        {
            vertRealWorldPositions[i] = quad.transform.TransformPoint(meshVerts[i]);
        }

        Vector3 midPoint = Vector3.Slerp(vertRealWorldPositions[0], vertRealWorldPositions[1], 0.5f);
        return midPoint;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        hoverPrefab = Instantiate(prefab);
        AdjustPrefabAlpha();
        hoverPrefab.SetActive(false);
    }
}
