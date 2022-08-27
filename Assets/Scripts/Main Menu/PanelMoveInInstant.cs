using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelMoveInInstant : MonoBehaviour
{
    public Transform panel;
    public Transform locationTarget;

    public void MoveIn()
    {
        panel.position = new Vector3(locationTarget.position.x, locationTarget.position.y, 0);
    }
}
