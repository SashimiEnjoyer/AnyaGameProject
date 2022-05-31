using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Collection, Health, Door}
public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject uiPopUp;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiPopUp.SetActive(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiPopUp.SetActive(false);
        }
    }
}
