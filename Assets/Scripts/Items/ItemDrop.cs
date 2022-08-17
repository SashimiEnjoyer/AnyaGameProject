using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject objectTarget;
    public GameObject objectDropping;
    private Vector3 location;
    private InteractableObject itemInteractable;

    void Update()
    {
        if (objectTarget != null)
        {
            location = objectTarget.transform.position;
            //objectDropping.transform.position = location  + new Vector3(0, 10001, 0);
            objectDropping.SetActive(false);
            objectDropping.transform.position = location + new Vector3(0, 1, 0);
        }
        
        else
        {
            //objectDropping.transform.position = location + new Vector3(0, 1, 0);
            objectDropping.SetActive(true);
            Destroy(this);
        }
    }
}
