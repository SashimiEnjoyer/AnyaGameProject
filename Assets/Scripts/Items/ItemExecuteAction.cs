using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExecuteAction : MonoBehaviour
{
    public DialogueController dialogueController;

    [Header("Objects to Destroy")]
    public GameObject objectDestroy;

    [Header("Objects to Appear")]
    public GameObject objectAppear;
    public float yAxisTransform;

    [Header("Objects to Teleport")]
    public GameObject objectTeleport;
    public GameObject objectTeleportLocation;
    public bool teleportTransition = false;

    private void Update()
    {
        if (dialogueController.DialogueEnd == true)
        {
            if (objectDestroy != null)
            Destroy(objectDestroy);

            if (objectAppear != null)
            objectAppear.transform.position += new Vector3(0, yAxisTransform, 0);

            if (objectTeleport != null && objectTeleportLocation != null)
            {
            teleportTransition = true;
            objectTeleport.transform.position = objectTeleportLocation.transform.position;
            }
        }
    }
}
