using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Dirt, tilledLand, watered
    }

    [SerializeField]
    private Material dirtMat, tilledLandMat, wateredMat;
    new Renderer renderer;
    public LandStatus landStatus;
    public GameObject selected;

    private Material currentMaterial;

    //Cache the time the land was watered
    GameTimestamp timeWatered;


    void Start()
    {
        renderer = GetComponent<Renderer>();

        //set default material for land status
        SwitchLandStatus(LandStatus.Dirt);
        Select(false);

        // Add this to TimeManager Listener List
        TimeManager.Instance.RegisterTracker(this);
    }

    // Update is called once per frame
    public void SwitchLandStatus(LandStatus status)
    {
        landStatus = status;
        Material material = dirtMat;
        switch (status)
        {
            case LandStatus.Dirt:
                material = dirtMat;
                break;
            case LandStatus.tilledLand:
                material = tilledLandMat;
                break;
            case LandStatus.watered:
                //renderer.material cant be used to compare directly as it is Instance, so create currentMaterial
                // only the tilled land can be watered
                if(currentMaterial == tilledLandMat)
                {
                    material = wateredMat;

                    //Cache the time it was watered
                    timeWatered = TimeManager.Instance.GetGameTimestamp();
                    
                }
                break;
        }

        renderer.material = currentMaterial = material;
    }

    public void Select(bool toggle)
    {
        selected.SetActive(toggle);
    }

    public void Interact()
    {
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        EquipmentData equipmentTool = toolSlot as EquipmentData;

        if(equipmentTool != null)
        {
            EquipmentData.ToolType toolType = equipmentTool.toolType;
            switch(toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.tilledLand);
                    break;
                case EquipmentData.ToolType.WateringCan:

                    SwitchLandStatus(LandStatus.watered);
                    break;
            }
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if(landStatus == LandStatus.watered)
        {
            // Hours since the land was watered
            int hourElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);
            
            
            if(hourElapsed > 24)
            {
                //Dry up (Switch Back to tilledLand)
                SwitchLandStatus(LandStatus.tilledLand);
            }
        }
    }
}
