using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityCollider : MonoBehaviour
{

    public List<GameObject> closeBlocksColl = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ProximityCollider")
        {
            //print("Enter - Collider tag: " + collider.tag);
            //print("BlockID: " + collider.GetComponentInParent<BlockPrim>().blockID);
            closeBlocksColl.Add(collider.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "ProximityCollider")
        {
            //print("Exit - Collider tag: " + collider.tag);
            //print("BlockID: " + collider.GetComponentInParent<BlockPrim>().blockID);
            closeBlocksColl.Remove(collider.transform.parent.gameObject);
        }
    }
}
