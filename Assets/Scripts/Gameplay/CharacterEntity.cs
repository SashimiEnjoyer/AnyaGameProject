using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { Move, Stop}

public class CharacterEntity : MonoBehaviour
{
    public Status characterStatus;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (characterStatus == Status.Stop)
            return;

        Debug.Log("Move");
        
    }
}
