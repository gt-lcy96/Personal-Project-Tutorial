using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // SingleTon
    public static InventoryManager Instance { get; private set; }

    [Header("Tools")]
    [SerializeField] 
    private ItemSlotData[] toolSlots = new ItemSlotData[8];
    [SerializeField] 
    private ItemSlotData equippedToolSlot = null;

    [Header("Items")]
    [SerializeField] 
    private ItemSlotData[] itemSlots = new ItemSlotData[8];
    [SerializeField] 
    private ItemSlotData equippedItemSlot = null;

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
        if(equippedItemSlot != null)
        {
            // Instantiate the item on the player's hand position and put it on the scene.
            Instantiate(GetEquippedSlotItem(InventorySlot.InventoryType.Item).gameModel, handPoint);
        }
    }
    #region Get and Checks
    //Get the slot item (ItemData)
    public ItemData GetEquippedSlotItem(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlot.itemData;
        }
        return equippedToolSlot.itemData;
    }

    //Get function for the slots (ItemSlotData)
    public ItemSlotData GetEquippedSlot(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlot;
        }
        return equippedToolSlot;
    }

    //Get function for the inventory slots
    public ItemSlotData[] GetInventorySlots(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return itemSlots;
        }
        return toolSlots;
    }

    //Check if an hand slot has an item
    public bool SlotEquipped(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlot != null;
        }
        return equippedToolSlot != null;
    }

    public bool IsTool(ItemData item)
    {
        // try to cast it as EquipmentData first
        EquipmentData equipment = item as EquipmentData;
        if(equipment != null)
        {
            return true;
        }

        //Try to cast it as a seed
        SeedData seed = item as SeedData;
        //If the seed is not null, it is a seed
        return seed != null;
    }

    #endregion

    //Only for equipping empty slots
    public void EquipEmptySlot(ItemData item)
    {
        if (IsTool(item))
        {
            equippedToolSlot = new ItemSlotData(item);
        } else
        {
            equippedItemSlot = new ItemSlotData(item);
        }
    }

    private void OnValidate()
    {
        ValidateInventorySlot(equippedToolSlot);
        ValidateInventorySlot(equippedItemSlot);

        ValidateInventorySlots(itemSlots);
        ValidateInventorySlots(toolSlots);
    }

    //When giving the itemData Value in the inspector, automatically set the quantity to 1
    void ValidateInventorySlot(ItemSlotData slot)
    {
        if(slot.itemData != null && slot.quantity == 0)
        {
            slot.quantity = 1;
        }
    }

    //Validate arrays
    void ValidateInventorySlots(ItemSlotData[] array)
    {
        foreach (ItemSlotData slot in array)
        {
            ValidateInventorySlot(slot);
        }
    }



    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType type)
    {
        if(type == InventorySlot.InventoryType.Item)
        {
            // Cache the Inventory slot ItemData from InventoryManager
            ItemSlotData itemToEquip = itemSlots[slotIndex];

            // Change the Inventory Slot to the Hand's
            itemSlots[slotIndex] = equippedItemSlot;

            // Change the Hand's Slot to Inventory
            equippedItemSlot = itemToEquip;

            //Update the item on hand changes in scene
            RenderItemOnHand();
        } else 
        {
            ItemSlotData toolToEquip = toolSlots[slotIndex];

            // Change the Inventory Slot to the Hand's
            toolSlots[slotIndex] = equippedToolSlot;

            // Change the Hand's Slot to Inventory
            equippedToolSlot = toolToEquip;
        }
    }

    public void HandToInventory(InventorySlot.InventoryType type)
    {
        if(type == InventorySlot.InventoryType.Item)
        {
            // Iterate through each inventory slot and find an empty slot to filled in
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i] == null)
                {
                    //Send the equipped item over to its new slot
                    itemSlots[i] = equippedItemSlot;
                    equippedItemSlot = null;
                    break;
                }
            }

            // Update item changes in scene
            RenderItemOnHand();
            
            
        } else {
             // Iterate through each inventory slot and find an empty slot to filled in
            for (int i = 0; i < toolSlots.Length; i++)
            {
                if(toolSlots[i] == null)
                {
                    //Send the equipped item over to its new slot
                    toolSlots[i] = equippedToolSlot;
                    equippedToolSlot = null;
                    break;
                }
            }
            
        }
    }
}
