using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton design
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    [Header("Tools")]
    public ItemData[] tools = new ItemData[8];
    public ItemData equippedTool = null;

    [Header("Items")]
    public ItemData[] items = new ItemData[8];
    public ItemData equippedItem = null;

    
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType type)
    {
        if(type == InventorySlot.InventoryType.Item)
        {
            // Cache the Inventory slot ItemData from InventoryManager
            ItemData itemToEquip = items[slotIndex];

            // Change the Inventory Slot to the Hand's
            items[slotIndex] = equippedItem;

            // Change the Hand's Slot to Inventory
            equippedItem = itemToEquip;
        } else 
        {
            ItemData toolToEquip = tools[slotIndex];

            // Change the Inventory Slot to the Hand's
            tools[slotIndex] = equippedTool;

            // Change the Hand's Slot to Inventory
            equippedTool = toolToEquip;
        }
    }

    public void HandToInventory(InventorySlot.InventoryType type)
    {
        if(type == InventorySlot.InventoryType.Item)
        {
            // Iterate through each inventory slot and find an empty slot to filled in
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i] == null)
                {
                    //Send the equipped item over to its new slot
                    items[i] = equippedItem;
                    equippedItem = null;
                    break;
                }
            }

            
            
        } else {
             // Iterate through each inventory slot and find an empty slot to filled in
            for (int i = 0; i < tools.Length; i++)
            {
                if(tools[i] == null)
                {
                    //Send the equipped item over to its new slot
                    tools[i] = equippedTool;
                    equippedTool = null;
                    break;
                }
            }
            
        }
    }
}
