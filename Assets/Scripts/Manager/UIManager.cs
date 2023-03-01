using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    public Image toolEquipSlot;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI toolQuantityText;
    
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


    [Header("Yes No Prompt")]
    public YesNoPrompt yesNoPrompt;

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

        // Add UIMAnAger to the list of objects timeMnaager will notify
        TimeManager.Instance.RegisterTracker(this);
    }

    void Update()
    {
        RenderInventory();
    }

    public void TriggerYesNoPrompt(string message, System.Action onYesCallback)
    {
        yesNoPrompt.gameObject.SetActive(true);

        yesNoPrompt.CreatePrompt(message, onYesCallback);
    }

#region Inventory
    public void AsignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }


    void RenderEquipTool(ItemData toolData)
    {
        // Text should be empty by default
        toolQuantityText.text = "";

        if(toolData != null)
        {
            toolEquipSlot.sprite = toolData.thumbnail;
            toolEquipSlot.gameObject.SetActive(true);

            int quantity = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool).quantity;
            if(quantity > 1)
            {
                toolQuantityText.text = quantity.ToString();
            }

            return;
        }
        

        toolEquipSlot.gameObject.SetActive(false);
    }
    
    public void RenderInventory()
    {
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);

        RenderInventoryPanel(toolSlots, inventoryToolSlots);
        RenderInventoryPanel(itemSlots, inventoryItemSlots);
        
        
        // Render the equipped slots in Inventory Panel
        toolHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        itemHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item));

        RenderEquipTool(InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool));
        
    }

    void RenderInventoryPanel(InventorySlot[] uiSlot, ItemSlotData[] itemData)
    {
        for (int i = 0; i < uiSlot.Length; i++)
        {
            uiSlot[i].Display(itemData[i]);
        }
    }


    public void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        RenderInventory();
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
#endregion

#region time
    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Handle the time
        int hours = timestamp.hour;
        string minutes =  timestamp.minute.ToString("00");

        //AM or PM
        string prefix = "AM";

        if(hours > 12)
        {
            // Time become PM
            prefix = "PM";
            hours -= 12;
        }

        timeText.text = $"{prefix} {hours}:{minutes}";

        // Handle the date
        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();

        // Format it for the date text display
        dateText.text = $"{season} {day} ({dayOfTheWeek})";
    }
#endregion
}
