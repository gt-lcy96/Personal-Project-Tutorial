using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // SingleTon
    public static InventoryManager Instance { get; private set; }

    [Header("Tools")]
    public ItemData[] tools = new ItemData[8];
    public ItemData equippedTool = null;

    [Header("Items")]
    public ItemData[] items = new ItemData[8];
    public ItemData equippedItem = null;

    public Transform handPoint;

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


    public void RenderItemOnHand()
    {   
        // Reset object on the hand
        if(handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }
        
        // Check if player has anything to equipped
        if(equippedItem != null)
        {
            // Instantiate the item on the player's hand position and put it on the scene.
            Instantiate(equippedItem.gameModel, handPoint);
        }
    }
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

            //Update the item on hand changes in scene
            RenderItemOnHand();
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

            // Update item changes in scene
            RenderItemOnHand();
            
            
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
