using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlotData
{
    
    public ItemData itemData;
    public int quantity;

    public ItemSlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        ValidateQuantity();
    }

    //Automatically construct the class with the item data of quantity 1
    public ItemSlotData(ItemData itemData)
    {
        this.itemData = itemData;
        quantity = 1;
        ValidateQuantity();
    }

    //Clones the ItemSlotData
    public ItemSlotData(ItemSlotData slotToClone)
    {
        itemData = slotToClone.itemData;
        quantity = slotToClone.quantity;
    }

    //Stacking System
    //Shortcut function to add 1 to the stack
    public void AddQuantity()
    {
        AddQuantity(1);
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    public void Remove()
    {
        quantity--;
        ValidateQuantity();
    }

    // Compare the item
    public bool Stackable(ItemSlotData slotToCompare)
    {
        return slotToCompare.itemData == itemData;
    }
    
    private void ValidateQuantity()
    {
        if (quantity <= 0 || itemData == null)
        {
            Empty();
        }
    }

    public void Empty()
    {
        itemData = null;
        quantity = 0;
    }

    public bool IsEmpty()
    {
        return itemData == null;
    }
}
