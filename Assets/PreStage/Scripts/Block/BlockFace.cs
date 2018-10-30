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
    public Vector3[] faceVerts;
    /// <summary>
    /// Face center location for the local center. To get the global position call transform.position
    /// </summary>
    public Vector3 FACE_CENTER
    {
        get
        {
            if(faceVerts.Length == 4)
            {
                Vector3 face_center = (faceVerts[0] + faceVerts[1] + faceVerts[2] + faceVerts[3]) / 4;
                return face_center;
            }
            else
            {
                return new Vector3();
            }
        }
    }
    /// <summary>
    /// These are the 4 container indexes that hold face's vertices and the adjacent face's verts that
    /// share the same location.
    /// </summary>
    public int[] vertexIndexCon;
    public Vector3 faceNormal;

	// Use this for initialization
	void Start () {
        //print(BLOCK_COMP.VERTS_COLL.Length);
        //if(BLOCK_COMP.)
        if(this.name == "face_neg_y")
        {
            foreach(int vert in vertexIndexCon)
            {
                //print(vert);
            }
            //print("Center: " + FACE_CENTER);
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateFaceLoc(BLOCK_COMP.selected);
        if (this.name != "face_pos_y" && this.name != "face_neg_y")
        {
            MoveFace(BLOCK_COMP.colliderName);
        }
        //if (this.name == "face_neg_y") print("Center: " + FACE_CENTER);
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method to move the face. Works only of the vertical faces.
    /// </summary>
    /// <param name="colliderName">Collider that is hit, if it is the same as this obj then run the function.</param>
    public void MoveFace(string colliderName)
    {
        if(this.name == colliderName)
        {
            Vector3 moveDir = faceNormal;

            for (int i = 0; i < vertexIndexCon.Length; i++)
            {
                // Get the container index (Check Rhino file for these indexes).
                int contIndex = vertexIndexCon[i];
                // Get the actual container that stores vertix index with the same coordinates.
                int[] vertexIndex = BLOCK_COMP.vertex_index_con[contIndex];
                for (int j = 0; j < vertexIndex.Length; j++)
                {
                    // Get the index of vertex from BLOCK_COMP.vertex_index_con - this represent 3 vertices 
                    // per container that share the same location
                    int index = vertexIndex[j];
                    // Get the actual vertex from mesh.
                    // When moving a face, in addition to its 4 vertices, this will move adjacent face's verts.
                    BLOCK_COMP.vertices[index] += moveDir * 0.05f;
                }
            }
            /// Update block vertices with freshly moved ones.
            BLOCK_COMP.block_mesh.vertices = BLOCK_COMP.vertices;
        }
    }

    //---------------------------------------------------------------------------------------------------
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

    //---------------------------------------------------------------------------------------------------
    void OnGUI()
    {
        GUI.color = Color.blue;
        Drawing.DrawLabel(transform.position, gameObject.name, 80);
    }

}
