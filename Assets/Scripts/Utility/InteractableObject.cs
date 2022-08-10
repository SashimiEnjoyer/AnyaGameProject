using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Collection, Health, Door, EnemyMassageBox}
public class InteractableObject : MonoBehaviour
{
    [Header("Pop Up Settings")]
    [SerializeField] protected GameObject uiPopUp;
    [SerializeField] float floatingHeight = 0.4f;
    [SerializeField] float floatingFreq = 1f;

    Vector2 popUpStartingPosition;
    Vector2 popUpMovingPosition;

    float floatingValue;
    float persistentPopUpHeight;

    private void Awake()
    {
        popUpStartingPosition = uiPopUp.transform.position;
    }
    private void Update()
    {
        if (uiPopUp.activeInHierarchy)
        {
            PopUpFloating();
            floatingValue += Time.deltaTime;
            floatingValue %= (2 * 3.14f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiPopUp.SetActive(true);
            popUpMovingPosition = popUpStartingPosition;    
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiPopUp.SetActive(false);
            floatingValue = 0;
        }
    }

    void PopUpFloating()
    {
        popUpMovingPosition.y = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + popUpStartingPosition.y;
        uiPopUp.transform.position = popUpMovingPosition;
    }
}
