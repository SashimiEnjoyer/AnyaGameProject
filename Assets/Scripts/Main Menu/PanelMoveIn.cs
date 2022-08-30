using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelMoveIn : MonoBehaviour
{
    public Transform panel;
    public Transform locationTarget;
    public float speed;
    public bool useEscapeKey;

    public void MoveIn()
    {
        panel.DOMove(new Vector3(locationTarget.position.x, locationTarget.position.y, 0), 1/speed).SetUpdate(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && useEscapeKey == true)
        MoveIn();
    }
}
