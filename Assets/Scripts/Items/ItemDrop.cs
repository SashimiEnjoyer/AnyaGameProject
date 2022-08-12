using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject objectTarget;
    public GameObject objectDropping;
    private Vector3 location;

    void Update()
    {
        if (objectTarget != null)
        {
            location = objectTarget.transform.position;
            objectDropping.transform.position = location  + new Vector3(0, 10001, 0);
        }
        
        if (objectTarget == null)
        {
            objectDropping.transform.position = location + new Vector3(0, 1, 0);
            Destroy(this);
        }
    }
}
