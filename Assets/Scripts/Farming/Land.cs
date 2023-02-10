using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public int id;
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

    [Header("Crops")]
    public GameObject cropPrefab;

    //crop currently not plant on ground
    CropBehaviour cropPlanted = null;

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
                if(currentMaterial == tilledLandMat || currentMaterial == wateredMat)
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
        ItemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        //if nothing equip, return early
        if(!InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            return;
        }

        TryInteractEquipmentTool(toolSlot);
        TryInteractSeedTool(toolSlot);
    }
    private void TryInteractEquipmentTool(ItemData toolSlot)
    {
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
                case EquipmentData.ToolType.Shovel:
                    if(cropPlanted != null)
                    {
                        //remove the crop from the land
                        Destroy(cropPlanted.gameObject);
                    }
                    break;
            }
        }
    }
    private void TryInteractSeedTool(ItemData toolSlot)
    {
        //Try casting the itemdata in the toolslot as SeedData
        SeedData seedTool = toolSlot as SeedData;
        
        //Conditions for the player to be able to plant a seed
        //1. He is holding a tool of type SeedData
        //2. The Land State must be either watered or tilled
        //3. There isn't already a crop that has been planted
        if(seedTool != null && landStatus != LandStatus.Dirt && cropPlanted == null)
        {
            GameObject cropObject = Instantiate(cropPrefab, transform);
            cropObject.transform.localPosition = new Vector3(0, 0.55f, 0);

            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            cropPlanted.Plant(seedTool);

            
            InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
            
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if(landStatus == LandStatus.watered)
        {
            // Hours since the land was watered
            int hourElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);
            
            // Grow the planted crop, if any
            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if(hourElapsed > 24)
            {
                //Dry up (Switch Back to tilledLand)
                SwitchLandStatus(LandStatus.tilledLand);
            }
        }

        // Handle the wilting of the plant
        if(landStatus == LandStatus.tilledLand && cropPlanted != null)
        {
            // If the crop has already germinated, start the withering
            if(cropPlanted.cropState != CropBehaviour.CropState.Seed)
            {
                cropPlanted.Wither();
            }
        }
    }


}
