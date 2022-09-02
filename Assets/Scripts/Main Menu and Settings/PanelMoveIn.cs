using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelMoveIn : MonoBehaviour
{
    public Transform panel;
    public Transform locationTarget;
    public float speed;
    PanelMoveIn panelMoveIn;
    public bool backButton;
    private float panelX;
    private float targetX;
    public InputKeyInteractable keyTrigger;

    void Update()
    {
        panelX = panel.localPosition.x;
        targetX = locationTarget.localPosition.x;

        if (panelX <= 1)
        backButton = true;
        
        if (panelX == targetX)
        backButton = false;

        if (keyTrigger != null)
        {
            if (backButton == true)
            keyTrigger.enabled = false;

            if (backButton == false)
            keyTrigger.enabled = true;
        }
    }

    public void MoveIn()
    {
        panel.DOMove(new Vector3(locationTarget.position.x, locationTarget.position.y, 0), 1/speed).SetUpdate(true);
    }

    public void BackButtonMoveIn()
    {
        if (backButton == true)
        MoveIn();
    }
}
