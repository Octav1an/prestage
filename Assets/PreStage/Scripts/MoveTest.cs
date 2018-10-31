using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {

    public Vector3 targetPos;
    const int MOUSE = 0;
    Vector3 saveLoc;
    Vector3 saveTarget;
    Plane plane;
    Ray ray;
    RaycastHit hit;
    string colliderName;
    bool mouseDown = false;


    // Use this for initialization1
    void Start()
    {
        targetPos = transform.position;
        saveLoc = transform.position;
        saveTarget = targetPos;
        plane = new Plane(Vector3.up, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(MOUSE))
        {
            SetTarggetPosition();
        }
        if (Input.GetMouseButtonDown(MOUSE))
        {
            saveLoc = transform.position;
            saveTarget = targetPos;
        }
        transform.position = saveLoc + (targetPos - saveTarget);

        if (Input.GetMouseButtonUp(MOUSE))
        {
            mouseDown = false;
        }
    }

    void SetTarggetPosition()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
        {
            if (Physics.Raycast(ray, out hit))
            {
                //print(hit.point);
                colliderName = hit.collider.name;
                if(hit.collider.name == "Cube")
                {
                    mouseDown = true;
                }
            }
            if (mouseDown)
            {
                targetPos = ray.GetPoint(point);
            }
        }
    }
}
