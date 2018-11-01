using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockPrim : MonoBehaviour {

    /// <summary>
    /// Block ID in the list that stores all existing blocks. This is not uniq, because
    /// the list can be dynamicaly updated.
    /// </summary>
    public int blockID = -1;

    //Vertex Array, use only geting the vertices. This property cannot rewrite them.
    public Vector3[] VERTS_COLL
    {
        get
        {
            return GetComponent<MeshFilter>().mesh.vertices;
        }
    }

    // Make them private later.
    public Vector3[] FACE_VERTS_POS_Z
    {
        get
        {
            Vector3[] face_pos_z = new Vector3[4];
            face_pos_z[0] = VERTS_COLL[0];
            face_pos_z[1] = VERTS_COLL[1];
            face_pos_z[2] = VERTS_COLL[2];
            face_pos_z[3] = VERTS_COLL[3];
            return face_pos_z;
        }
    }
    public Vector3[] FACE_VERTS_NEG_Z
    {
        get
        {
            Vector3[] face_neg_z = new Vector3[4];
            face_neg_z[0] = VERTS_COLL[8];
            face_neg_z[1] = VERTS_COLL[9];
            face_neg_z[2] = VERTS_COLL[10];
            face_neg_z[3] = VERTS_COLL[11];
            return face_neg_z;
        }
    }
    public Vector3[] FACE_VERTS_POS_X
    {
        get
        {
            Vector3[] face_pos_x = new Vector3[4];
            face_pos_x[0] = VERTS_COLL[12];
            face_pos_x[1] = VERTS_COLL[13];
            face_pos_x[2] = VERTS_COLL[14];
            face_pos_x[3] = VERTS_COLL[15];
            return face_pos_x;
        }
    }
    public Vector3[] FACE_VERTS_NEG_X
    {
        get
        {
            Vector3[] face_neg_x = new Vector3[4];
            face_neg_x[0] = VERTS_COLL[4];
            face_neg_x[1] = VERTS_COLL[5];
            face_neg_x[2] = VERTS_COLL[6];
            face_neg_x[3] = VERTS_COLL[7];
            return face_neg_x;
        }
    }
    public Vector3[] FACE_VERTS_POS_Y
    {
        get
        {
            Vector3[] face_pos_y = new Vector3[4];
            face_pos_y[0] = VERTS_COLL[20];
            face_pos_y[1] = VERTS_COLL[21];
            face_pos_y[2] = VERTS_COLL[22];
            face_pos_y[3] = VERTS_COLL[23];
            return face_pos_y;
        }
    }
    public Vector3[] FACE_VERTS_NEG_Y
    {
        get
        {
            Vector3[] face_neg_y = new Vector3[4];
            face_neg_y[0] = VERTS_COLL[16];
            face_neg_y[1] = VERTS_COLL[17];
            face_neg_y[2] = VERTS_COLL[18];
            face_neg_y[3] = VERTS_COLL[19];
            return face_neg_y;
        }
    }
    public Vector3[][] BLOCK_FACE_VERTS
    {
        get
        {
            Vector3[][] block_face_coll = new Vector3[6][];
            block_face_coll[0] = FACE_VERTS_POS_Z;
            block_face_coll[1] = FACE_VERTS_NEG_Z;
            block_face_coll[2] = FACE_VERTS_POS_X;
            block_face_coll[3] = FACE_VERTS_NEG_X;
            block_face_coll[4] = FACE_VERTS_POS_Y;
            block_face_coll[5] = FACE_VERTS_NEG_Y;
            return block_face_coll;
        }
    }

    // Faces objects.
    public GameObject FACE_POS_Z_OBJ
    {
        get;
        private set;
    }
    public GameObject FACE_NEG_Z_OBJ
    {
        get;
        private set;
    }
    public GameObject FACE_POS_X_OBJ
    {
        get;
        private set;
    }
    public GameObject FACE_NEG_X_OBJ
    {
        get;
        private set;
    }
    public GameObject FACE_POS_Y_OBJ
    {
        get;
        private set;
    }
    public GameObject FACE_NEG_Y_OBJ
    {
        get;
        private set;
    }

    // Face Script components.
    public BlockFace FACE_POS_Z
    {
        get
        {
            if(FACE_POS_Z_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_POS_Z"));
            }
            else
            {
                return FACE_POS_Z_OBJ.GetComponent<BlockFace>();
            }
            
        }
    }
    public BlockFace FACE_NEG_Z
    {
        get
        {
            if (FACE_NEG_Z_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_NEG_Z"));
            }
            else
            {
                return FACE_NEG_Z_OBJ.GetComponent<BlockFace>();
            }

        }
    }
    public BlockFace FACE_POS_X
    {
        get
        {
            if (FACE_POS_X_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_POS_X"));
            }
            else
            {
                return FACE_POS_X_OBJ.GetComponent<BlockFace>();
            }

        }
    }
    public BlockFace FACE_NEG_X
    {
        get
        {
            if (FACE_NEG_X_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_NEG_X"));
            }
            else
            {
                return FACE_NEG_X_OBJ.GetComponent<BlockFace>();
            }

        }
    }
    public BlockFace FACE_POS_Y
    {
        get
        {
            if (FACE_POS_Y_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_POS_Y"));
            }
            else
            {
                return FACE_POS_Y_OBJ.GetComponent<BlockFace>();
            }

        }
    }
    public BlockFace FACE_NEG_Y
    {
        get
        {
            if (FACE_NEG_Y_OBJ == null)
            {
                throw (new CannotSetUpFaceException("Face Setup failed - FACE_NEG_Y"));
            }
            else
            {
                return FACE_NEG_Y_OBJ.GetComponent<BlockFace>();
            }

        }
    }
    /// <summary>
    /// Stores all BlockFace components of this Block.
    /// Used to easier update or get data in for loops.
    /// </summary>
    public BlockFace[] FACE_COLL
    {
        get
        {
            BlockFace[] coll = new BlockFace[] {
                FACE_POS_Z, FACE_NEG_Z,
                FACE_POS_X, FACE_NEG_X,
                FACE_POS_Y, FACE_NEG_Y
            };
            return coll;
        }
    }

    /// <summary>
    /// This represend collection of indexes for vertices that share the same coordinate. Think about them as Block's 8 vertices.
    /// Check Rhino file to see to which container each vertex belongs.
    /// </summary>
    public int[][] vertex_index_con = new int[][]
    {
        new int[] {0, 7, 21},
        new int[] {1, 6, 19},
        new int[] {2, 13, 18},
        new int[] {3, 12, 22},
        new int[] {8, 15, 23},
        new int[] {9, 14, 17},
        new int[] {10, 5, 16},
        new int[] {11, 4, 20}
    };
    
    public bool selected = false;

    Ray ray;
    RaycastHit hit;
    /// <summary>
    /// Plane that is used to move the object, it is equal to block centroid.
    /// </summary>
    private Plane movePlane;
    /// <summary>
    /// Saved the location of intersection between mouse ray with movePlane. Used for moving the block.
    /// </summary>
    public Vector3 savedMoveTarget;
    /// <summary>
    /// Intersection position between plane and mouse ray that is used for horizontal movements of block and faces.
    /// </summary>
    public Vector3 TARGET_WORLD
    {
        get
        {
            return SetTarggetPosition();
        }
    }

    /// <summary>
    /// Block's mesh component.
    /// </summary>
    public Mesh block_mesh;
    /// <summary>
    /// Block mesh vertices, use this to transform vertices location.
    /// </summary>
    public Vector3[] vertices;
    /// <summary>
    /// Saved block's vertices, used to create the movement vector.
    /// </summary>
    public Vector3[] verticesSaved;
    /// <summary>
    /// Saved location of the block. It is saved during MouseDown event.
    /// </summary>
    private Vector3 savedBlockLoc;
    /// <summary>
    /// String that stores the name of the hit collider untill the mouse is released.
    /// </summary>
    public String colliderName;


    // Use this for initialization
    void Start () {
        //--------------------------------------------
        savedBlockLoc = this.transform.position;
        block_mesh = GetComponent<MeshFilter>().mesh;
        vertices = block_mesh.vertices;
        verticesSaved = block_mesh.vertices;
        if (blockID == -1) blockID = Manager.COLL_BLOCKS_OBJECTS.IndexOf(gameObject);
        savedMoveTarget = SetTarggetPosition();
        movePlane = new Plane(Vector3.up, this.transform.position);
        SetUpIndividualFaces();
        //--------------------------------------------
        foreach (Vector3 vertex in vertices)
        {
            //Debug.Log(vertex);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Vector3 moveDir = block_mesh.normals[0];

            for (int i = 0; i < 3; i++)
            {
                //vertices[i] += moveDir * 0.05f;
                vertices[vertex_index_con[0][i]] += moveDir * 0.05f;
                vertices[vertex_index_con[1][i]] += moveDir * 0.05f;
                vertices[vertex_index_con[2][i]] += moveDir * 0.05f;
                vertices[vertex_index_con[3][i]] += moveDir * 0.05f;
            }
            // Update block vertices with freshly moved ones.
            block_mesh.vertices = vertices;

        }
    }

    private void LateUpdate()
    {
        // By having these function inside LateUpdate - I make sure that first the select boolean is triggered by 
        // Manager and then run these function.
        // Here run methods when the block is selected only.
        if (selected)
        {
            UpdateFaceVerts();
            OnMouseUpLocal();
            OnMouseDownLocal();
            
            // Here run everything that should run on mouse down.
            if (Input.GetMouseButton(0))
            {
                MoveBlock();
                RotateBlock();
            }
        }
    }


    //---------------------------------------------MOUSE UP-------------------------------------------------------
    /// <summary>
    /// Method that is activated once when the mouse right click is released and the block is selected.
    /// </summary>
    private void OnMouseUpLocal()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Reset the collider name to empty.
            colliderName = "";
            verticesSaved = block_mesh.vertices;
            // Reset the selection
            selected = false;
        }
        
    }

    //---------------------------------------------MOUSE DOWN------------------------------------------------------
    /// <summary>
    /// Method that is activated once when the mouse right click is pressed and the block is selected.
    /// </summary>
    private void OnMouseDownLocal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print("BlockID: " + blockID);
            savedBlockLoc = this.transform.position;
            savedMoveTarget = SetTarggetPosition();

            //-------------------------------------------------------
            // Update the colliderName when MouseDown.
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                colliderName = hit.collider.name;
                
            }
            //-------------------------------------------------------
            // Save location for several things inside BlockFace. Like FaceCenter.
            foreach (BlockFace face in FACE_COLL)
            {
                face.SaveOnMouseDown();
            }
        } 
    }

    private void RotateBlock()
    {
        if (colliderName == "face_pos_y")
        {
            if (Input.GetKey(KeyCode.N))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
            }
            else if (Input.GetKey(KeyCode.M))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * (-50), Space.World);
            }
        }

    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method that returns the intersection point between object's middle plane and a ray from mouse position.
    /// </summary>
    /// <returns></returns>
    private Vector3 SetTarggetPosition()
    {
        Ray rayPlane = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (movePlane.Raycast(rayPlane, out point))
        {
            // Return the point in world space that intersects with this plane.
            return rayPlane.GetPoint(point);
        }
        else
        {
            return new Vector3();
        }
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Move the entire block, when click and drag on the top face.
    /// </summary>
    private void MoveBlock()
    {
        if(colliderName == "face_pos_y")
        {
            //if(Input.GetMouseButton(0)) SetTarggetPosition();
            transform.position = savedBlockLoc + (SetTarggetPosition() - savedMoveTarget);
        }
    }

    //---------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method that runs when the block is selected. It updates the faces vertices and their normals.
    /// </summary>
    private void UpdateFaceVerts()
    {
        // Assign Vertices to each face
        FACE_POS_Z.faceVerts = FACE_VERTS_POS_Z;
        FACE_NEG_Z.faceVerts = FACE_VERTS_NEG_Z;
        FACE_POS_X.faceVerts = FACE_VERTS_POS_X;
        FACE_NEG_X.faceVerts = FACE_VERTS_NEG_X;
        FACE_POS_Y.faceVerts = FACE_VERTS_POS_Y;
        FACE_NEG_Y.faceVerts = FACE_VERTS_NEG_Y;

        // Assign FaceNormals (Update)
        FACE_POS_Z.faceNormal = block_mesh.normals[0];
        FACE_NEG_Z.faceNormal = block_mesh.normals[8];
        FACE_POS_X.faceNormal = block_mesh.normals[12];
        FACE_NEG_X.faceNormal = block_mesh.normals[4];
        FACE_POS_Y.faceNormal = block_mesh.normals[20];
        FACE_NEG_Y.faceNormal = block_mesh.normals[16];
    }

    //---------------------------------------------------------------------------------------------------
    private void SetUpIndividualFaces()
    {
        // Find and assign face objects to properties in this class.
        try
        {
            FACE_POS_Z_OBJ = transform.Find("FACE_POS_Z".ToLower()).gameObject;
            FACE_NEG_Z_OBJ = transform.Find("FACE_NEG_Z".ToLower()).gameObject;
            FACE_POS_X_OBJ = transform.Find("FACE_POS_X".ToLower()).gameObject;
            FACE_NEG_X_OBJ = transform.Find("FACE_NEG_X".ToLower()).gameObject;
            FACE_POS_Y_OBJ = transform.Find("FACE_POS_Y".ToLower()).gameObject;
            FACE_NEG_Y_OBJ = transform.Find("FACE_NEG_Y".ToLower()).gameObject;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Problem with Face setup in BlockPrim/SetUpIndividualFaces()");
        }
        // Assign Vertices to each face (Update)
        FACE_POS_Z.faceVerts = FACE_VERTS_POS_Z;
        FACE_NEG_Z.faceVerts = FACE_VERTS_NEG_Z;
        FACE_POS_X.faceVerts = FACE_VERTS_POS_X;
        FACE_NEG_X.faceVerts = FACE_VERTS_NEG_X;
        FACE_POS_Y.faceVerts = FACE_VERTS_POS_Y;
        FACE_NEG_Y.faceVerts = FACE_VERTS_NEG_Y;

        // Assign FaceNormals (Update)
        FACE_POS_Z.faceNormal = block_mesh.normals[0];
        FACE_NEG_Z.faceNormal = block_mesh.normals[8];
        FACE_POS_X.faceNormal = block_mesh.normals[12];
        FACE_NEG_X.faceNormal = block_mesh.normals[4];
        FACE_POS_Y.faceNormal = block_mesh.normals[20];
        FACE_NEG_Y.faceNormal = block_mesh.normals[16];

        // Assign vertexIndexContainer - these are the arrays indexes that hold vertices of the block. (One Time assignment)
        FACE_POS_Z.vertexIndexCon = new int[] { 0, 1, 2, 3 };
        FACE_NEG_Z.vertexIndexCon = new int[] { 4, 5, 6, 7 };
        FACE_POS_X.vertexIndexCon = new int[] { 2, 3, 4, 5 };
        FACE_NEG_X.vertexIndexCon = new int[] { 0, 1, 6, 7 };
        FACE_POS_Y.vertexIndexCon = new int[] { 0, 3, 4, 7 };
        FACE_NEG_Y.vertexIndexCon = new int[] { 1, 2, 5, 6 };


    }

    void OnGUI()
    {
        GUI.color = new Color(1f, 0.1f, 0f, 1f);
        if (selected) GUI.Label(new Rect(20, 0, 220, 100), ("Selected Block ID: " + this.blockID));

        GUI.color = new Color(1f, 0.5f, 0f, 1f);
        Vector3 mouseLoc = Manager.CHANGE_IN_MOUSE_LOC;
        GUI.Label(new Rect(20, 20, 220, 100), ("Diff mouse loc - " + "x: " + mouseLoc.x + " y: " + mouseLoc.y + " z: " + mouseLoc.z));
        Vector3 mouseLocWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        //Vector3 mouseLocWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GUI.Label(new Rect(20, 35, 400, 100), ("Diff_m_world - " + "x: " + mouseLocWorld.x + " y: " + mouseLocWorld.y + " z: " + mouseLocWorld.z));

        GUI.color = new Color(0.2f, 0.1f, 0.9f, 1f);
        //GUI.Label(new Rect(20, 35, 400, 100), ("BlockID: " + blockID));
        Drawing.DrawLabel(FACE_POS_Y.FACE_CENTER_WORLD + new Vector3(0, 0.3f , 0), "BlockID: " + blockID);
        //Vector2 scre = Camera.main.WorldToScreenPoint(FACE_POS_Y.FACE_CENTER_WORLD);
        //Vector2 scre2 = Camera.main.WorldToScreenPoint(FACE_POS_Y.FACE_CENTER_WORLD + new Vector3(0, 0.3f, 0));
        //Drawing.DrawLine(new Vector2(scre.x, Screen.height - scre.y), new Vector2(scre2.x, Screen.height - scre2.y), Color.red, 2);




        GUI.color = Color.red;
        //Drawing.DrawLabel(vertices[0], "V_0", this.gameObject);
        //Drawing.DrawLabel(vertices[1], "V_1", this.gameObject);
        //Drawing.DrawLabel(vertices[2], "V_2", this.gameObject);
        //Drawing.DrawLabel(vertices[3], "V_3", this.gameObject);

        //Drawing.DrawLabel(vertices[8], "V_4", this.gameObject);
        //Drawing.DrawLabel(vertices[9], "V_5", this.gameObject);
        //Drawing.DrawLabel(vertices[10], "V_6", this.gameObject);
        //Drawing.DrawLabel(vertices[11], "V_7", this.gameObject);

        /*
        GUI.color = Color.green;
        DrawLabel(vertices[4], "V_4");
        DrawLabel(vertices[5], "V_5");
        DrawLabel(vertices[6], "V_6");
        DrawLabel(vertices[7], "V_7");
        
        GUI.color = Color.green;
        DrawLabel(vertices[8], "V_8");
        DrawLabel(vertices[9], "V_9");
        DrawLabel(vertices[10], "V_10");
        DrawLabel(vertices[11], "V_11");
        
        GUI.color = Color.green;
        DrawLabel(vertices[12], "V_12");
        DrawLabel(vertices[13], "V_13");
        DrawLabel(vertices[14], "V_14");
        DrawLabel(vertices[15], "V_15");
        
        GUI.color = Color.green;
        DrawLabel(vertices[16], "V_16");
        DrawLabel(vertices[17], "V_17");
        DrawLabel(vertices[18], "V_18");
        DrawLabel(vertices[19], "V_19");
        
        GUI.color = Color.green;
        DrawLabel(vertices[20], "V_20");
        DrawLabel(vertices[21], "V_21");
        DrawLabel(vertices[22], "V_22");
        DrawLabel(vertices[23], "V_23");
        */

    }

}
