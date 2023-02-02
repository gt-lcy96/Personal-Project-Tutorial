using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public GameObject inventoryPanel;

    [Header("Inventory System")]
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;
    public static UIManager Instance { get; private set; }

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
        RenderInventory();
    }

    void RenderInventory()
    {
        RenderInventoryPanel(toolSlots, InventoryManager.Instance.tools);
        RenderInventoryPanel(itemSlots, InventoryManager.Instance.items);
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

}