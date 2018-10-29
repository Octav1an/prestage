using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockPrim : MonoBehaviour {

    //Vertex Array
    public Vector3[] VERTS_COLL
    {
        get
        {
            return GetComponent<MeshFilter>().mesh.vertices;
        }
    }

    //Make them private later
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

    // Faces objects
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

    public bool selected = true;

    private Camera cam;
    Ray ray;
    RaycastHit hit;
    Vector3 lastSavedLoc;
    float deltaLoc = 0f;
    bool mouseDown = false;

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] verticesSaved;

    float anglePrj = 0;
    Vector2 projNorm = new Vector2();

    int[] triangels;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        lastSavedLoc = new Vector3();
        //deltaLoc = new Vector3();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        verticesSaved = mesh.vertices;
        triangels = mesh.triangles;
        foreach (Vector3 vertex in vertices)
        {
            //Debug.Log(vertex);
        }
        for(int i = 0; i < mesh.normals.Length; i++)
        {
            //Debug.Log("Vertex: " + i + "  Normal: " + mesh.normals[i]);
        }
        SetUpIndividualFaces();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Here run methods when the block is selected only
        if (selected)
        {
            UpdateFaceVerts();
        }


        if (Input.GetKey(KeyCode.R))
        {
            Vector3 moveDir = mesh.normals[0];
            vertices[0] += moveDir * 0.05f;
            vertices[1] += moveDir * 0.05f;
            vertices[2] += moveDir * 0.05f;
            vertices[3] += moveDir * 0.05f;

            //vertices[8] += moveDir * 0.05f;
            //vertices[9] += moveDir * 0.05f;

            mesh.vertices = vertices;

        }
        
        if (Input.GetMouseButtonDown(0))
        {
            
            lastSavedLoc = Input.mousePosition;
            verticesSaved = mesh.vertices;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //print(hit.collider.name);
                mouseDown = true;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        //print(mesh.normals[6]);

        if (Input.GetMouseButton(0))
        {
            Vector3 projected = Vector3.ProjectOnPlane(mesh.normals[0], Camera.main.transform.forward);
            projNorm = new Vector2(100 + (projected.x * 30), 100 + (projected.y * 30));

            Vector3 mouseLoc = Input.mousePosition;
            deltaLoc = mouseLoc.x - lastSavedLoc.x;
            Vector3 moveVec = (mouseLoc - lastSavedLoc).normalized;
            Vector3 moveVecProject = Vector3.ProjectOnPlane(moveVec, Camera.main.transform.forward);
            anglePrj = Vector3.Angle(moveVecProject, projected);
            
            if (mouseDown)
            {
                if (projected.x < 0)
                {
                    //MoveFace(deltaLoc);
                    //MoveFace(mouseLoc - lastSavedLoc);
                }
                else
                {
                    //MoveFace(-deltaLoc);
                    //MoveFace(mouseLoc - lastSavedLoc);
                }
                
                //print(deltaLoc);
            }
        }

    }



    void OnGUI()
    {
        GUI.color = new Color(1f, 0.5f, 0f, 1f);
        GUI.Label(new Rect(20, 5, 100, 100), anglePrj.ToString());

        Vector2 vec = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        Drawing.DrawLine(new Vector2(100, 100), projNorm, Color.red, 2);

        
        GUI.color = Color.red;
        DrawLabel(vertices[0], "V_0");
        DrawLabel(vertices[1], "V_1");
        DrawLabel(vertices[2], "V_2");
        DrawLabel(vertices[3], "V_3");
        
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


    void MoveFace(float moveDist)
    {
        Vector3 moveDir = mesh.normals[0].normalized * moveDist / 100;
        vertices[0] = verticesSaved[0] + moveDir;
        vertices[1] = verticesSaved[1] + moveDir;
        vertices[2] = verticesSaved[2] + moveDir;
        vertices[3] = verticesSaved[3] + moveDir;

        vertices[8] = verticesSaved[8] + moveDir;
        vertices[9] = verticesSaved[9] + moveDir;

        vertices[13] = verticesSaved[13] + moveDir;
        vertices[14] = verticesSaved[14] + moveDir;

        vertices[16] = verticesSaved[16] + moveDir;
        vertices[17] = verticesSaved[17] + moveDir;

        vertices[22] = verticesSaved[22] + moveDir;
        vertices[23] = verticesSaved[23] + moveDir;
        mesh.vertices = vertices;
        GameObject colliderFace = this.transform.Find("Face_0").gameObject;
        colliderFace.transform.position = transform.TransformPoint((vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4);
    }

    void MoveFace(Vector3 vec)
    {
        Vector3 moveDir = mesh.normals[0].normalized * vec.magnitude / 100;
        vertices[0] = verticesSaved[0] + moveDir;
        vertices[1] = verticesSaved[1] + moveDir;
        vertices[2] = verticesSaved[2] + moveDir;
        vertices[3] = verticesSaved[3] + moveDir;

        vertices[8] = verticesSaved[8] + moveDir;
        vertices[9] = verticesSaved[9] + moveDir;

        vertices[13] = verticesSaved[13] + moveDir;
        vertices[14] = verticesSaved[14] + moveDir;

        vertices[16] = verticesSaved[16] + moveDir;
        vertices[17] = verticesSaved[17] + moveDir;

        vertices[22] = verticesSaved[22] + moveDir;
        vertices[23] = verticesSaved[23] + moveDir;
        mesh.vertices = vertices;
        GameObject colliderFace = this.transform.Find("Face_0").gameObject;
        colliderFace.transform.position = transform.TransformPoint((vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4);
    }

    /// <summary>
    /// Draw a label text in specified location in 3D, used for Debuging.
    /// </summary>
    /// <param name="loc"> 3D location of the label text. </param>
    /// <param name="text"> String with text to display. </param>
    void DrawLabel(Vector3 loc, string text)
    {
        Vector2 guiPosition = Camera.main.WorldToScreenPoint(transform.TransformPoint(loc));
        guiPosition.y = Screen.height - guiPosition.y;
        GUI.Label(new Rect(guiPosition, new Vector2(30, 20)), text);
    }

    void MoveTest()
    {
        vertices[0] -= Vector3.back * 0.05f;
        vertices[1] -= Vector3.back * 0.05f;
        vertices[2] -= Vector3.back * 0.05f;
        vertices[3] -= Vector3.back * 0.05f;

        vertices[8] -= Vector3.back * 0.05f;
        vertices[9] -= Vector3.back * 0.05f;
        //print(Vector3.up * 0.2f);
        mesh.vertices = vertices;
        //mesh.RecalculateBounds();
        mesh.triangles = triangels;
    }

    private void UpdateFaceVerts()
    {
        // Assign Vertices to each face
        FACE_POS_Z.faceVerts = FACE_VERTS_POS_Z;
        FACE_NEG_Z.faceVerts = FACE_VERTS_NEG_Z;
        FACE_POS_X.faceVerts = FACE_VERTS_POS_X;
        FACE_NEG_X.faceVerts = FACE_VERTS_NEG_X;
        FACE_POS_Y.faceVerts = FACE_VERTS_POS_Y;
        FACE_NEG_Y.faceVerts = FACE_VERTS_NEG_Y;
    }

    //----------------------------------------------------------------------------------------------
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
        // Assign Vertices to each face
        FACE_POS_Z.faceVerts = FACE_VERTS_POS_Z;
        FACE_NEG_Z.faceVerts = FACE_VERTS_NEG_Z;
        FACE_POS_X.faceVerts = FACE_VERTS_POS_X;
        FACE_NEG_X.faceVerts = FACE_VERTS_NEG_X;
        FACE_POS_Y.faceVerts = FACE_VERTS_POS_Y;
        FACE_NEG_Y.faceVerts = FACE_VERTS_NEG_Y;
    }

}
