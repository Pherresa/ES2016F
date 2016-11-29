﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    GameObject hoverPrefab;
    public Slot[] Slots;
    GameManager gm;
    //GameObject activeSlot;

    Slot activeSlot;
    Action_Defense prefabActionDefense;
    LifeAmountManager lifeAmountManager;
    GameObject auraPrefab;

    public AudioClip soundDrop;
    public AudioClip soundDragging;
    private AudioSource source {
        get{
            //MainCamera mc = GameObject.FindObjectOfType(typeof(MainCamera)) as MainCamera;
            return Camera.main.GetComponent<AudioSource> ();
            //return mc.GetComponent<AudioSource> ();

        }
    }
    void playSound(AudioClip audio){
        source.PlayOneShot (audio);
    }




    /**
     * Prefab Unit instantation still not active, ready to be drag 
     * */
    void Start()
    {
        auraPrefab = Resources.Load("Prefabs/AreaProjector") as GameObject;
        Slots = FindObjectsOfType(typeof(Slot)) as Slot[];

        prefabActionDefense = prefab.GetComponent<Action_Defense>();
        lifeAmountManager  = GameObject.FindObjectOfType<LifeAmountManager>();
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
        if (lifeAmountManager.amount >= prefabActionDefense.towerPrice)
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
                    playSound(soundDragging);
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
        if (lifeAmountManager.amount >= prefabActionDefense.towerPrice)
        {
            if (activeSlot != null)
            {
                // MeshFilter mf = activeSlot.GetComponent<MeshFilter> ();
                if (!activeSlot.getIsPath() && !activeSlot.isOccupied)
                {
                    Vector3 quadCentre = GetQuadCentre(activeSlot.gameObject);
                    GameObject newUnit = (GameObject)Instantiate(prefab, quadCentre, Quaternion.identity);
                    Action_Defense actionDefense = newUnit.GetComponent<Action_Defense>();

                    actionDefense.activate();
                    lifeAmountManager.LoseAmount(actionDefense.towerPrice);

                    foreach (ParticleSystem particleSystem in newUnit.GetComponentsInChildren<ParticleSystem>())
                    {
                        particleSystem.Play();
                    }

                    GameObject aura = Instantiate(auraPrefab);
                    aura.GetComponent<Projector>().enabled = false;
                    aura.transform.position = newUnit.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
                    aura.transform.parent = newUnit.transform;

                    playSound(soundDrop);

                    activeSlot.isOccupied = true;
                    activeSlot.unit = newUnit;
                    activeSlot.GetComponent<MeshRenderer>().enabled = false;

                }
                else
                {
                    activeSlot.SetActive(false);
                }
            }
            else
            {
                Destroy(hoverPrefab);
            }
            // Then set it to inactive ready for the next drag!
            hoverPrefab.SetActive(false);
        }
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

        if(lifeAmountManager.amount >= prefabActionDefense.towerPrice)
        {
            hoverPrefab = Instantiate(prefab);
            AdjustPrefabAlpha();
            hoverPrefab.SetActive(false);
            GameObject aura = Instantiate(auraPrefab);
            aura.GetComponent<Projector>().orthographicSize = 35;
            aura.transform.position = hoverPrefab.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
            aura.transform.parent = hoverPrefab.transform;

        }

    }
}
