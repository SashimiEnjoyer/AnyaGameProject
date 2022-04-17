using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Collection, Health}
public class CollectableItems : MonoBehaviour, IInteractable
{

    public ItemType itemType;

    public void ExecuteInteractable()
    {
        switch (itemType)
        {
            case ItemType.Health:
                HealItem();
                break;
        }

        Destroy(gameObject);
    }

    void HealItem()
    {
        PlayerStats.instance.playerHealth += 10;
    }
}
