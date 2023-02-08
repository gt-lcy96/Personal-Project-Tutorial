using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowableHarvestBehaviour : InteractableObject
{
    CropBehaviour parentCrop;

    public void SetParent(CropBehaviour parentCrop)
    {
        this.parentCrop = parentCrop;
    }

    // Start is called before the first frame update
    public override void Pickup()
    {
        //Update Item on Player's hand 
        InventoryManager.Instance.EquipHandSlot(item);
        InventoryManager.Instance.RenderItemOnHand();

        parentCrop.Regrow();
    }
}
