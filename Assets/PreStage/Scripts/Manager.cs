using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager instance;

    public static List<GameObject> COLL_BLOCKS_OBJECTS = new List<GameObject>();
    /// <summary>
    /// Store location of mouse when right click is pressed.
    /// </summary>
    public static Vector3 savedMouseLoc = new Vector3();
    /// <summary>
    /// Get the vector that represents difference between last mouse location and current mouse location
    /// </summary>
    public static Vector3 CHANGE_IN_MOUSE_LOC
    {
        get
        {
            if(savedMouseLoc.x == 0 && savedMouseLoc.y == 0)
            {
                return new Vector3();
            }
            else
            {
                return Input.mousePosition - savedMouseLoc;
            }
        }
    }
    public static GameObject GROUND
    {
        get
        {
            return GameObject.FindGameObjectWithTag("Ground");
        }
    }
    public GameObject blockPrefab;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        // Makes sure that I use always a game control even if my next scence already has one.
        // The instance of the object from the scene that is current will persist in the next scene.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        CountAndStoreBlocks();
    }

    // Use this for initialization
    void Start () {
        //print(COLL_BLOCKS_OBJECTS.Count);
    }
	
	// Update is called once per frame
	void Update () {
        //--------------------------------------------
        // Run all the time.
        //SaveMouseLocation();
        OnMouseDownGlobal();
        OnMouseUpGlobal();
        //--------------------------------------------
        foreach (GameObject obj in COLL_BLOCKS_OBJECTS)
        {
            //print(obj.GetComponent<BlockPrim>().selected);
        }
    }

    //-------------------------------------------------EVENTS--------------------------------------------------
    // By having the events for mouse down and up here, solved the problem of not activating the Input.GetMouseDown or Up
    // because, I think this event runs first. Moved everything to LateUpdate in BlockPrim, and works.
    private void OnEnable()
    {
        //EventManager.MouseDownGlobal += OnMouseDownGlobal;
        //EventManager.MouseUpGlobal += OnMouseUpGlobal;
    }
    private void OnDisable()
    {
        //EventManager.MouseDownGlobal -= OnMouseDownGlobal;
        //EventManager.MouseUpGlobal -= OnMouseUpGlobal;
    }
    //---------------------------------------------------------------------------------------------------

    //---------------------------------------------MOUSE UP-------------------------------------------------------
    /// <summary>
    /// Method that is activated once when the mouse right click is released and the block is selected.
    /// </summary>
    private void OnMouseUpGlobal()
    {
        if (Input.GetMouseButtonUp(0))
        {

        }
    }

    //---------------------------------------------MOUSE DOWN------------------------------------------------------
    /// <summary>
    /// Method that is activated once when the mouse right click is pressed and the block is selected.
    /// </summary>
    private void OnMouseDownGlobal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //-------------------------------------------------------
            // Update the colliderName when MouseDown.
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                SelectBlock(hit);
            }

        }
    }

    public void CreateBlock()
    {
        GameObject fresh_obj = (GameObject)Instantiate(blockPrefab, new Vector3(), Quaternion.identity);
        COLL_BLOCKS_OBJECTS.Add(fresh_obj);
    }

    void SelectBlock(RaycastHit _hit)
    {
        if (hit.collider.tag == "BlockFace")
        {
            BlockPrim block = hit.collider.gameObject.GetComponent<BlockFace>().BLOCK_COMP;
            block.selected = true;
            //print(block.selected);
            //print(hit.collider.gameObject.GetComponent<BlockFace>().blockID);
        }
        else
        {
            Debug.Log("I don't know what are u hitting.");
        }
    }

    private void SaveMouseLocation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            savedMouseLoc = Input.mousePosition;
        }
    }

    public static Vector3 GetProjectedOnGround(Vector3 loc)
    {
        if (loc != null)
        {
            BoxCollider groundCollider = GROUND.GetComponent<BoxCollider>();
            Vector3 closestPoint = groundCollider.ClosestPoint(loc);
            return closestPoint;
        }
        else
        {
            return new Vector3();
        }

    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Searches and store the blocks that are already drawn in COLL_BLOCKS_OBJECTS list.
    /// It should be used only once at scene starup, later objects shoud be dynamicaly added and removed. 
    /// </summary>
    private void CountAndStoreBlocks()
    {
        GameObject[] alreadyCreated = GameObject.FindGameObjectsWithTag("BlockPrim");
        foreach (GameObject obj in alreadyCreated)
        {
            COLL_BLOCKS_OBJECTS.Add(obj);
        }

    }

}
