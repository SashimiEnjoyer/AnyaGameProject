using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType { Collection, Health, Door, EnemyMassageBox}
public class InteractableObject : MonoBehaviour, IInteractable
{

    [Header("Pop Up Settings")]
    [Tooltip("If Ui PopUp prefab is null, this game object will be floating")]
    [SerializeField] public bool isFloating;
    [SerializeField] protected GameObject uiPopUp;

    [SerializeField] float floatingHeight = 0.4f;
    [SerializeField] float floatingFreq = 1f;

    [Header("Custom Event")]
    public UnityEvent onInteracting;

    Vector2 startingPosition;
    Vector2 movingPosition;
    float floatingValue;

    private void Awake()
    {
        startingPosition = uiPopUp != null? uiPopUp.transform.position : transform.position;
        movingPosition = startingPosition;
    }

    private void Update()
    {
        if (!isFloating)
            return;

        PopUpFloating();
        floatingValue += Time.deltaTime;
        floatingValue %= (2 * 3.14f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (uiPopUp != null)
            {
                uiPopUp.SetActive(true);
                movingPosition = startingPosition;
            }
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (uiPopUp != null)
            {
                uiPopUp.SetActive(false);
                floatingValue = 0;
            }
        }
    }

    protected void ExecuteInteractableEvent()
    {
        onInteracting?.Invoke();
    }

    void PopUpFloating()
    {
        movingPosition.y = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + startingPosition.y;

        if (uiPopUp != null)
            uiPopUp.transform.position = movingPosition;
        else
            transform.position = movingPosition;
    }

    public void ExecuteInteractable()
    {
        ExecuteInteractableEvent();
    }
}
