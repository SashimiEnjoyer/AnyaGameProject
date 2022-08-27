using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryScript : MonoBehaviour
{
    public PanelMoveIn script;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        script.MoveIn();
    }
}
