using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Collection, Health, Door}
public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject uiPopUp;
    GameObject goTemp;
    List<GameObject> uiPopUps = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            goTemp = Instantiate(uiPopUp);
            uiPopUps.Add(goTemp);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject g in uiPopUps)
                Destroy(g);

            uiPopUps.Clear();
        }
    }
}
