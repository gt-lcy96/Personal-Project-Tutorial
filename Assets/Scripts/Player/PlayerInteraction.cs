using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;
    public Vector3 rayCastDirection = Vector3.down;
    private float raycastDistance = 2;
    private RaycastHit hit;
    private Land selectedLand;

    InteractableObject selectedInteractable = null;
    // Start is called before the first frame update
    void Start()
    {
        // playerController = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, rayCastDirection);
        Debug.DrawRay(transform.position, rayCastDirection * raycastDistance);
        
        if(Physics.Raycast(ray, out hit, raycastDistance)) 
        {
            OnInteractableHit(hit);
            // Debug.Log("Hit object" + hit.collider.name);
        }
    }

    void OnInteractableHit(RaycastHit hit)
    {

        Collider other = hit.collider;
        if(other.gameObject.CompareTag("Soil"))
        {
            HandleLandSelection(other.GetComponent<Land>());
            return;
        }

        // Deselect the land if player is not standing on any land
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }

        //---- handle Select Item ----
        if(other.tag == "Item") 
        {
            //Set the interactable to the currently selected interactable
            selectedInteractable = other.GetComponent<InteractableObject>();
            return;
        }
        
        // Deselect the interactable if player is not standing on anything infront at the moment
        if(selectedInteractable != null)
        {
            selectedInteractable = null;
        }
    }

    void HandleLandSelection(Land land)
    {
        // Deactive previous selection if any
        if(selectedLand != null)
        {
            selectedLand.Select(false);
        }

        selectedLand = land;
        land.Select(true);
    }

    public void Interact()
    {
        if(selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }

    }

    public void ItemInteract()
    {
        //If the player is holding something, keep it in his Inventory
        if(InventoryManager.Instance.equippedItem != null)
        {
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }

        //If the player isn't holding anything, pick up the item
        if (selectedInteractable != null)
        {
            selectedInteractable.Pickup();
        }
         

    }
}
