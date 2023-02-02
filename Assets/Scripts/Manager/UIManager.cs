using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    public Image toolEquipSlot;
    
    [Header("Inventory System")]
    public GameObject inventoryPanel;

    // The tool equip slot UI on the Inventory panel
    public HandInventorySlot toolHandSlot;

    // The item equip slot UI on the Inventory panel
    public HandInventorySlot itemHandSlot;

    // the ui slot for tools and item
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;
    

    //Item info box
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

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
    private void Start() {
        inventoryPanel.SetActive(false);
        AsignSlotIndexes();
    }

    public void AsignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    void Update()
    {
        RenderInventory();
    }
    void RenderEquipTool(ItemData toolData)
    {
        if(toolData != null)
        {
            toolEquipSlot.sprite = toolData.thumbnail;
            toolEquipSlot.gameObject.SetActive(true);

            Debug.Log("render equiped tool");
            return;
        }

        toolEquipSlot.gameObject.SetActive(false);
    }
    
    void RenderInventory()
    {
        RenderInventoryPanel(toolSlots, InventoryManager.Instance.tools);
        RenderInventoryPanel(itemSlots, InventoryManager.Instance.items);
        RenderEquipTool(InventoryManager.Instance.equippedTool);
        
        // Render the equipped slots in Inventory Panel
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);
    }

    void RenderInventoryPanel(InventorySlot[] uiSlot, ItemData[] itemData)
    {
        for (int i = 0; i < uiSlot.Length; i++)
        {
            uiSlot[i].Display(itemData[i]);
        }
    }


    public void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void DisplayItemInfo(ItemData data)
    {
        if(data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";

            return;
        }   

        itemNameText.text = data.name;
        itemDescriptionText.text = data.descripton;
    }
    
}