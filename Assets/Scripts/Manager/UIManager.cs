using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject inventoryPanel;

    [Header("Inventory System")]
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