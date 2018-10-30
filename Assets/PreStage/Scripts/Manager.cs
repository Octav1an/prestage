using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //--------------------------------------------
        // Run all the time.
        SaveMouseLocation();

        //--------------------------------------------
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

}
