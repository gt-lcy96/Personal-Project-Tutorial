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
        if(SlotEquipped(InventorySlot.InventoryType.Item))
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
            return !equippedItemSlot.IsEmpty();
        }
        return !equippedToolSlot.IsEmpty();
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

    //Equip the hand slot with an ItemData (Will overwrite the slot)
    public void EquipHandSlot(ItemData item)
    {
        if (IsTool(item))
        {
            equippedToolSlot = new ItemSlotData(item);
        } else
        {
            equippedItemSlot = new ItemSlotData(item);
        }
    }

    //Equip the hand slot with an ItemData (Will overwrite the slot)
    public void EquipHandSlot(ItemSlotData itemSlot)
    {
        if (IsTool(itemSlot.itemData))
        {
            equippedToolSlot = new ItemSlotData(itemSlot);
        } else
        {
            equippedItemSlot = new ItemSlotData(itemSlot);
        }
    }

#region Inventory Slot Validation
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
#endregion

    
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        //The slot to equip (Tool by default)
        ItemSlotData handToEquip = equippedToolSlot;
        ItemSlotData[] inventoryToAlter = toolSlots;

        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            // Change the slot to item
            handToEquip = equippedItemSlot;
            inventoryToAlter = itemSlots;
        }

        //Check if stackable
        if(handToEquip.Stackable(inventoryToAlter[slotIndex]))
        {
            ItemSlotData slotToAlter = inventoryToAlter[slotIndex];

            //Add to hand slot
            handToEquip.AddQuantity(slotToAlter.quantity);

            slotToAlter.Empty();

        } else
        {
            //Not stackable
            //Cache the Inventory itemSlotData
            ItemSlotData slotToEquip = new ItemSlotData(inventoryToAlter[slotIndex]);

            //Change the inventory slot to the hands
            inventoryToAlter[slotIndex] = new ItemSlotData(handToEquip);
            
            //Change the hand's slot to the Inventory Slot's
            EquipHandSlot(slotToEquip);

           
        }

        if(inventoryType == InventorySlot.InventoryType.Item)
        {

            //Update the changes in scene
            RenderItemOnHand();
        }
        //Update the changes to UI
        UIManager.Instance.RenderInventory();

    }

    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        ItemSlotData handSlot = equippedToolSlot;
        ItemSlotData[] inventoryToAlter = toolSlots;

        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            handSlot = equippedItemSlot;
            inventoryToAlter = itemSlots;
        }

        //Try stackig the hand slot.
        //Check if the operation failed
        if (!StackItemToInventory(handSlot, inventoryToAlter))
        {
            // Iterate through each inventory slot and find an empty slot to filled in
            for (int i = 0; i < inventoryToAlter.Length; i++)
            {
                if(inventoryToAlter[i].IsEmpty())
                {
                    //Send the equipped item over to its new slot
                    inventoryToAlter[i] = new ItemSlotData(handSlot);
                    handSlot.Empty();
                    break;
                }
            }
        }
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            // Update item changes in scene
            RenderItemOnHand();
        }   
            
        UIManager.Instance.RenderInventory();
    }

    //Iterate through each of the items in the inventory to see if it can be stacked
    // Will perform the operation if found, returns false if unsuccessful
    public bool StackItemToInventory(ItemSlotData itemSlot, ItemSlotData[] inventoryArray)
    {   
        for (int i = 0; i < inventoryArray.Length; i++)
        {
            if(inventoryArray[i].Stackable(itemSlot))
            {
                //Add to inventory slot's stack
                inventoryArray[i].AddQuantity(itemSlot.quantity);
                itemSlot.Empty();
                return true;
            }
        }

        return false;
    }
}
