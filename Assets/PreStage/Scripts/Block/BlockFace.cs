using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFace : MonoBehaviour
{

    public int blockID
    {
        get
        {
            return BLOCK_COMP.blockID;
        }
    }
    public BlockPrim BLOCK_COMP
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
            if (faceVerts.Length == 4)
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
    /// Face center location for world space.
    /// </summary>
    public Vector3 FACE_CENTER_WORLD
    {
        get
        {
            return BLOCK_COMP.transform.TransformPoint(FACE_CENTER);
        }
    }
    private Vector3 savedFaceCenter;
    /// <summary>
    /// Saved center location when the face was clicked. Used for moving face verts.
    /// </summary>
    private Vector3 SAVED_FACE_CENTER_WORLD
    {
        get
        {
            return BLOCK_COMP.transform.TransformPoint(savedFaceCenter);
        }
    }
    /// <summary>
    /// Indexes of the mid edge points of the face, setup in the BlockPrim.
    /// </summary>
    public int[] edgeMidCollIndex;
    /// <summary>
    /// Edge mid points of this face, referenced from parent BlockPrim.
    /// </summary>
    public Vector3[] FACE_EDGE_MID_COLL
    {
        get
        {
            Vector3[] coll = new Vector3[2];
            coll[0] = BLOCK_COMP.EDGE_MID_COLL[edgeMidCollIndex[0]];
            coll[1] = BLOCK_COMP.EDGE_MID_COLL[edgeMidCollIndex[1]];
            return coll;
        }
    }
    /// <summary>
    /// These are the 4 container indexes that hold face's vertices and the adjacent face's verts that
    /// share the same location.
    /// </summary>
    public int[] vertexIndexCon;
    public Vector3 faceNormal;
    /// <summary>
    /// Get the face normal direction in world space.
    /// </summary>
    public Vector3 FACE_NORMAL_WORLD
    {
        get
        {
            return BLOCK_COMP.gameObject.transform.TransformDirection(faceNormal);
        }
    }
    /// <summary>
    /// Return vector that is the projection of the target vector, for move porpuses.
    /// </summary>
    private Vector3 PROJECTED_TARGET
    {
        get
        {
            Vector3 prj = Vector3.Project(BLOCK_COMP.TARGET_WORLD, FACE_NORMAL_WORLD);
            return BLOCK_COMP.transform.position + prj;
        }
    }
    /// <summary>
    /// Save the PROJECTED_TARGET in order to create the offset necessary when starting to move the face.
    /// Getting the difference between PROJECTED_TARGET and savedProjectedTarget with cancel the jumping effect.
    /// </summary>
    private Vector3 savedProjectedTarget;


    // Use this for initialization
    void Start()
    {
        savedFaceCenter = FACE_CENTER;
        savedProjectedTarget = PROJECTED_TARGET;
        if (this.name == "face_neg_y")
        {
            foreach (int vert in vertexIndexCon)
            {
                //print(vert);
            }
            //print("Center: " + FACE_CENTER);
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFaceLoc(BLOCK_COMP.selected);
    }

    private void LateUpdate()
    {
        // Runs after void Update.
        if (BLOCK_COMP.selected)
        {
            if (this.name != "face_pos_y" && this.name != "face_neg_y")
            {
                
                MoveFace(BLOCK_COMP.colliderName);
                for (int i = 0; i < ((List<GameObject>)Snap()[0]).Count; i++)
                {
                    MoveFace(BLOCK_COMP.colliderName, MoveSnapFace(0.1f, 0.1f, i));
                }
                //BLOCK_COMP.UpdateBlockCollider();
                //BLOCK_COMP.UpdateProximityCollider();
                if (this.name == "face_neg_z")
                {
                    List<GameObject> list = (List<GameObject>)Snap()[0];
                    //print((float)list[0].GetComponent<BlockPrim>().MoveSnapBuildup(0)[4]);
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method that is called in BlockPrim when MouseDown event was triggered.
    /// </summary>
    public void SaveOnMouseDown()
    {
        savedFaceCenter = FACE_CENTER;
        savedProjectedTarget = PROJECTED_TARGET;
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method to move the face. Works only of the vertical faces.
    /// </summary>
    /// <param name="colliderName">Collider that is hit, if it is the same as this obj then run the function.</param>
    public void MoveFace(string colliderName)
    {
        if (this.name == colliderName)
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

                    //BLOCK_COMP.vertices[index] += moveDir * 0.05f;

                    // First get the vector between savedProjectedTarget and actualProjectedTarget, this is done in
                    // order to avoid jumping the face location when the mouse is clicked.
                    Vector3 diff = PROJECTED_TARGET - savedProjectedTarget;
                    // Before adding the "diff" vector convert it to local sapace, otherwise it won't work when the block is rotated.
                    BLOCK_COMP.vertices[index] = BLOCK_COMP.verticesSaved[index] + BLOCK_COMP.transform.InverseTransformVector(diff);
                    // If snap face is active move it accordingly
                }
            }
            /// Update block vertices with freshly moved ones.
            BLOCK_COMP.block_mesh.vertices = BLOCK_COMP.vertices;
            BLOCK_COMP.UpdateProximityCollider();
        }
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method to move the face. Works only of the vertical faces.
    /// </summary>
    /// <param name="colliderName">Collider that is hit, if it is the same as this obj then run the function.</param>
    public void MoveFace(string colliderName, Vector3 move)
    {
        if (this.name == colliderName)
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

                    //BLOCK_COMP.vertices[index] += moveDir * 0.05f;

                    // First get the vector between savedProjectedTarget and actualProjectedTarget, this is done in
                    // order to avoid jumping the face location when the mouse is clicked.
                    
                    Vector3 diff = (PROJECTED_TARGET + move) - savedProjectedTarget;
                    //BLOCK_COMP.UpdateBlockCollider();
                    // Before adding the "diff" vector convert it to local sapace, otherwise it won't work when the block is rotated.
                    BLOCK_COMP.vertices[index] = BLOCK_COMP.verticesSaved[index] + BLOCK_COMP.transform.InverseTransformVector(diff);
                    // If snap face is active move it accordingly
                }
            }
            /// Update block vertices with freshly moved ones.
            BLOCK_COMP.block_mesh.vertices = BLOCK_COMP.vertices;
            BLOCK_COMP.UpdateProximityCollider();
        }
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// 0. Proximity objects list
    /// 1. Move Vector, used to move the object
    /// 2. Closest projected vector
    /// 3. Closest projected vector index, i need it to get vector from this object
    /// 4. Closest distance (float)
    /// 5. Array with projections on the proximity objects.
    /// </summary>
    /// <param name="proxiIndex"></param>
    /// <returns></returns>
    private List<object> Snap(int proxiIndex = 0)
    {
        List<object> returnObj = new List<object>();
        // 1.Get the collider from the positive proximity distance
        List<GameObject> closeBlocksColl = BLOCK_COMP.PROXI_COLLIDER.closeBlocksColl;
        returnObj.Add(closeBlocksColl);
        // 2.Find the closest loc for each edge mid pt on each collider.
        Vector3 closestVec = new Vector3();
        float closestDist = 1000;
        int closestIndex = -1;
        Vector3[] coll_closest = new Vector3[2];

        if (closeBlocksColl.Count > 0)
        {
            // Check the closest vector from this to the proximity objects.
            for (int i = 0; i < FACE_EDGE_MID_COLL.Length; i++)
            {
                coll_closest[i] = closeBlocksColl[proxiIndex].GetComponent<BoxCollider>().ClosestPoint(FACE_EDGE_MID_COLL[i]);
                if ((coll_closest[i] - FACE_EDGE_MID_COLL[i]).magnitude < closestDist)
                {
                    closestDist = (coll_closest[i] - FACE_EDGE_MID_COLL[i]).magnitude;
                    closestVec = FACE_EDGE_MID_COLL[i];
                    closestIndex = i;
                }
            }
        }
        // The vector between EdgePt and projection on ProxiObj of this EdgePt.
        Vector3 moveVector = new Vector3();
        if (closeBlocksColl.Count > 0)
        {
            moveVector = coll_closest[closestIndex] - closestVec;
        }
        returnObj.Add(moveVector);
        returnObj.Add(closestVec);
        returnObj.Add(closestIndex);
        returnObj.Add(closestDist);
        returnObj.Add(coll_closest);

        return returnObj;
    }

    //---------------------------------------------------------------------------------------------------
    private Vector3 MoveSnapFace(float snapDist, float cornerSnap, int proxiIndex)
    {
        List<GameObject> list = (List<GameObject>)Snap()[0];
        float closestDist = (float)Snap(proxiIndex)[4];

        if (list.Count == 0)
        {
            return new Vector3();
        }
        //--------------------------------------------------------------------------
        // Find the closest edge of this obj and the coresponded closest edge of proxi obj that fits
        // the snapDist comparison. (This part is used in the Corner Snap only.)
        float cornerSnapDist = 1000;
        Vector3 closestEdge = new Vector3();
        Vector3 closestEdgeProxi = new Vector3();
        for (int i = 0; i < FACE_EDGE_MID_COLL.Length; i++)
        {
            Vector3 edgeMidThis = FACE_EDGE_MID_COLL[i];
            for (int j = 0; j < list[proxiIndex].GetComponent<BlockPrim>().EDGE_MID_COLL.Length; j++)
            {
                Vector3 edgeMidProxi = list[proxiIndex].GetComponent<BlockPrim>().EDGE_MID_COLL[j];
                if ((edgeMidThis - edgeMidProxi).magnitude < cornerSnapDist)
                {
                    cornerSnapDist = (edgeMidThis - edgeMidProxi).magnitude;
                    closestEdge = edgeMidThis;
                    closestEdgeProxi = list[proxiIndex].GetComponent<BlockPrim>().EDGE_MID_COLL[j];
                }
            }
        }
        //--------------------------------------------------------------------------
        // 1. Corner snap has the most priority.
        if (cornerSnapDist < cornerSnap)
        {
            // Project the move vector on the face normal, to avoid shift and break the block right angles.
            Vector3 move = Vector3.Project(closestEdgeProxi - closestEdge, FACE_NORMAL_WORLD);
            print("Zero [" + proxiIndex + "]: " + move.magnitude);
            return move;
        }
        // 2. Apply face snap from this as priority.
        // Doesn't properly work.
        else if (closestDist < snapDist)
        {
            // Specify the proxiIndex in order for Snap() to correctly calculate closest distance.
            Vector3 move = Vector3.Project((Vector3)Snap(proxiIndex)[1], FACE_NORMAL_WORLD);
            print("First [" + proxiIndex + "]: " + move.magnitude);
            return move;
        }
        // 3. Apply face snap from other proxi objects as priority.
        else if (Vector3.Project((Vector3)Snap(proxiIndex)[1], FACE_NORMAL_WORLD).magnitude < snapDist)
        {
            Vector3 move = (Vector3)Snap(proxiIndex)[1];
            print("Second [" + proxiIndex + "]: " + Vector3.Project(move, FACE_NORMAL_WORLD).magnitude);
            // Project the move vector from Snap() on FaceNormal, otherwise it will move diagonaly.
            return Vector3.Project(move, FACE_NORMAL_WORLD);
        }
        else
        {
            //print("No snap!");
            return new Vector3();
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
        //Drawing.DrawLabel(transform.position, gameObject.name, 80);
    }

}
