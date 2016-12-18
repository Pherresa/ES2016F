using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

	public bool isPath;
	public bool isOccupied;
	public bool isWater;
	public bool isTowerSlot;
	public GameObject unit;

	GameObject dragHandler;
	GameManager gameManager;
	public GameObject prefab;
    GameObject auraPrefab;

	public Slot activeSlot;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
		isOccupied = false;
		unit = null;

        auraPrefab = Resources.Load("Prefabs/AreaProjector") as GameObject;

        gameManager  = GameObject.FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getIsOccupied(){
		return isOccupied;
	}

	public bool getIsTowerSlot(){
		return isTowerSlot;
	}

	public bool getIsPath(){
		return isPath;
	}

	public bool getIsWater(){
		return isWater;
	}

	public void SetActive(bool active){
		gameObject.SetActive(active);
	}

	public void setDragHandler(GameObject drag){
		dragHandler = drag;
	}

	public void setPrefab(GameObject pf){
		prefab = pf;
	}


    void OnMouseDown() {
    	if(isTowerSlot && !isOccupied){
    		//GameObject.Find("DragHandler")
    		//dragHandler.
    		//DragHandler drag = FindObjectOfType(typeof(DragHandler)) as DragHandler;
    		posicionarUnidad();
    	}

    	else{

        	GameObject.Find("ButtonSell").GetComponent<Button>().onClick.RemoveAllListeners();
        	GameObject.Find("ButtonSell").GetComponent<Button>().onClick.AddListener(sell_obj);
        }
    }

    private void sell_obj() {
        GameObject.Find("GameManager").GetComponent<GameManager>().GainAmount(unit.GetComponent<Action_Defense>().getValues().towerPrice / 2);
        isOccupied = false;
        Destroy(unit);

    }

    void OnTriggerEnter(Collider other)
    {
    }


    public void posicionarUnidad(){
    	activeSlot = this;
		Vector3 quadCentre = GetQuadCentre(activeSlot.gameObject);
        GameObject newUnit = (GameObject)Instantiate(prefab, quadCentre, Quaternion.identity);
        Action_Defense actionDefense = newUnit.GetComponent<Action_Defense>();

        actionDefense.activate();
        gameManager.LoseAmount(newUnit.GetComponent<Action_Defense>().getTowerPrice());

        foreach (ParticleSystem particleSystem in newUnit.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }

        GameObject aura = Instantiate(auraPrefab);

        //prefabActionDefense.initTowerValues();
        aura.GetComponent<Projector>().orthographicSize = newUnit.GetComponent<Action_Defense>().getValues().range;//prefabActionDefense.range; 
        aura.GetComponent<Projector>().enabled = false;
        aura.transform.position = newUnit.transform.position + new Vector3(0.0f, 30.0f, 0.0f);
        aura.transform.parent = newUnit.transform;

        //playSound(soundDrop);

        activeSlot.isOccupied = true;
        activeSlot.unit = newUnit;
        activeSlot.GetComponent<MeshRenderer>().enabled = false;
        disableTowerSlots();

		if(newUnit.name.Contains("Gandalf")){
			newUnit.GetComponent<Gandalf> ().startAnimation ();
            newUnit.transform.Rotate(0,140,0);
		}
        else{
            newUnit.transform.Rotate(0,120,0);
        }




    }

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

    public void disableTowerSlots(){
    	Slot[] towerSlots = FindObjectsOfType(typeof(Slot)) as Slot[];
        foreach (Slot tSlot in towerSlots){
            //print ("towerSlot");
                tSlot.GetComponent<MeshRenderer> ().enabled = false;
                

        }
    }



}
