using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    bool infoShowed;
    float timeLeft;

    public GameObject prefab;
    GameObject hoverPrefab;
    public Slot[] Slots;
    GameManager gm;
    //GameObject activeSlot;

    Slot activeSlot;
    Action_Defense prefabActionDefense;
    LifeAmountManager lifeAmountManager;
    GameObject auraPrefab;
    GameObject ablePrefab;
    Texture red;
    Texture green;

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
        timeLeft = 2.0f;
        infoShowed = false;
        auraPrefab = Resources.Load("Prefabs/AreaProjector") as GameObject;
        ablePrefab = Resources.Load("Prefabs/ableToDropProjector") as GameObject;
        //red = Resources.Load("StandardAssets/")
        Slots = FindObjectsOfType(typeof(Slot)) as Slot[];

        prefabActionDefense = prefab.GetComponent<Action_Defense>();
        obt_price(prefabActionDefense);
        lifeAmountManager  = GameObject.FindObjectOfType<LifeAmountManager>();
    }


    void Update(){
        if(infoShowed){
            timeLeft-=Time.deltaTime;
        }
        if(timeLeft<0){
            timeLeft = 2.0f;
            infoShowed = false;
            GameObject info = GameObject.Find("ToBuyInfo");
            //info.transform.position = new Vector3(-100.0f, -100.0f, 0.0f);
            Vector3 v = info.transform.position;

            info.transform.position = new Vector3(-2000.0f, v.y,v.z);
            //info.SetActive(false);
        }

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
                    //Projector p = hoverPrefab.findChuk
                    
                    GameObject slotQuadObject = hits[slotIndex].collider.gameObject;
                    Slot slotQuad = slotQuadObject.GetComponent<Slot>();
                    activeSlot = slotQuad;
                    EnableSlot(slotQuad);
                }
                else
                {
                    hoverPrefab.GetComponentsInChildren<Projector>()[1].material.color = Color.red;
                    activeSlot = null;
                    DisableAllSlots();
                
                    alreadyPlayedDraggingSound = false;
                }
            }
        }
    }

    bool alreadyPlayedDraggingSound = false;

    void EnableSlot(Slot slot)
    {
        foreach (Slot availableSlot in Slots)
        {
            if (slot.name.Equals(availableSlot.name))
            {
				if(availableSlot.getIsPath() || availableSlot.isOccupied){
					availableSlot.GetComponent<MeshRenderer> ().enabled = true;
					availableSlot.GetComponent<Renderer> ().material.color = Color.red;
                    hoverPrefab.GetComponentsInChildren<Projector>()[1].material.color = Color.red;
                }
				else{
					availableSlot.GetComponent<MeshRenderer> ().enabled = true;
					availableSlot.GetComponent<Renderer> ().material.color = Color.green;
                    hoverPrefab.GetComponentsInChildren<Projector>()[1].material.color = Color.green;
                    if (!alreadyPlayedDraggingSound)
                    {
                        playSound(soundDragging);
                        alreadyPlayedDraggingSound = true;
                    }
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
                    lifeAmountManager.LoseAmount(prefabActionDefense.towerPrice);

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
            GameObject able = Instantiate(ablePrefab);
            //TODO: Harm zone get by the prefab defense class.
            aura.GetComponent<Projector>().orthographicSize = 35;
            aura.transform.position = hoverPrefab.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
            able.transform.position = hoverPrefab.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
            aura.transform.parent = hoverPrefab.transform;
            able.transform.parent = hoverPrefab.transform;

        }

    }



    public void buttonClicked(int index){

/*
        timeLeft=2.0f;
        float incx = 40.0f;
        float incy = 100.0f;

        GameObject boton1 = null;
        GameObject info = GameObject.Find("ToBuyInfo");



        if (index==1){
            boton1 = GameObject.Find("ButtonUnit1");
        }
        
        if(index==2){
            boton1 = GameObject.Find("ButtonUnit1");
            incx +=115.0f;

        }


        Vector3 v = boton1.transform.position;
        //info.transform.position = new Vector3(50.0f, 150.0f, 0.0f);
        info.transform.position = new Vector3(v.x+incx, v.y, v.z);
        infoShowed = true;
*/
        float xbase = GameObject.Find("ButtonUnit1").transform.position.x - 55.0f;


        timeLeft=2.0f;
        GameObject info = GameObject.Find("ToBuyInfo");
        float xposition = xbase + 105.0f*index;
        Vector3 v = info.transform.position;
        info.transform.position = new Vector3(xposition,v.y,v.z);

        infoShowed = true;

    }



    private void obt_price(Action_Defense actionDefense) {
        switch (actionDefense.towerTama)
        {
            case 1:
                prefabActionDefense.towerPrice = (int)Enemy_Values_Gene.m_little_tower("m");
                break;
            case 2:
                prefabActionDefense.towerPrice = (int)Enemy_Values_Gene.m_medium_tower("m");
                break;
            case 3:
                prefabActionDefense.towerPrice = (int)Enemy_Values_Gene.m_big_tower("m");
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}
