using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project : MonoBehaviour {

    Mesh mesh;
    public Vector3 location;

    // Use this for initialization
    void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        print(mesh.normals[0]);
        print(mesh.vertices[0]);
    }
	
	// Update is called once per frame
	void Update () {
        //print(mesh.normals[0]);
        //print(Vector3.ProjectOnPlane(new Vector3(0,0,0), mesh.normals[0]) + transform.TransformPoint(mesh.vertices[0]));
	}

    public void OnDrawGizmos()
    {
        var collider = GetComponent<Collider>();

        if (!collider)
        {
            return; // nothing to do without a collider
        }

        Vector3 closestPoint = collider.ClosestPoint(location);

        //print(closestPoint);

        Gizmos.DrawSphere(location, 0.1f);
        Gizmos.DrawWireSphere(closestPoint, 0.1f);
    }
}
