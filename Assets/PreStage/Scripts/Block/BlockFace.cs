using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFace : MonoBehaviour {

    
    private BlockPrim BLOCK_COMP
    {
        get
        {
            return transform.GetComponentInParent<BlockPrim>();
        }
    }
    /// <summary>
    /// Face center location for the local center. To get the global position call transform.position
    /// </summary>
    private Vector3 FACE_CENTER
    {
        get
        {
            Vector3 face_center = (faceVerts[0] + faceVerts[1] + faceVerts[2] + faceVerts[3]) / 4;
            return face_center;
        }
    }
    public Vector3[] faceVerts;


	// Use this for initialization
	void Start () {
        //print(BLOCK_COMP.VERTS_COLL.Length);
        //if(BLOCK_COMP.)
        if(this.name == "face_pos_z")
        {
            foreach(Vector3 vert in faceVerts)
            {
                //print(transform.TransformPoint(vert));
                
            }
            //print("Center: " + transform.position);
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateFaceLoc(BLOCK_COMP.selected);
    }

    /// <summary>
    /// When the cube is selected constanly update the this.transform to match the FACE_CENTER.
    /// </summary>
    /// <param name="selected">Boolean from BlockPrim class, is true if the block is selected.</param>
    public void UpdateFaceLoc(bool selected)
    {
        if (selected)
        {
            this.transform.localPosition = FACE_CENTER;
        }
    }

    void OnGUI()
    {
        GUI.color = Color.blue;
        Drawing.DrawLabel(transform.position, gameObject.name, 80);
    }

}
