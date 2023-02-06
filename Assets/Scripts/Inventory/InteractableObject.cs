using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // The item information the GameObject is supposed to represent
    public ItemData item;
    
    public virtual void Pickup()
    {
        InventoryManager.Instance.equippedItem = item;
        InventoryManager.Instance.RenderItemOnHand();
        
        //Destroy this instance so as dont have multiple copies
        Destroy(gameObject);
    }
}
