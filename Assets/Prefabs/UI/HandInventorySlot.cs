using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandInventorySlot : InventorySlot
{
    // Start is called before the first frame update
    public override void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.HandToInventory(inventoryType);
    }
}
