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
            Debug.Log("Hit object" + hit.collider.name);
        }
    }

    void OnInteractableHit(RaycastHit hit)
    {

        Collider other = hit.collider;
        if(other.gameObject.CompareTag("Soil"))
        {
            Debug.Log("selected");
            HandleLandSelection(other.GetComponent<Land>());
            return;
        }

        // Deselect the land if player is not standing on any land
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
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

        Debug.Log("Not on any land");
    }
}
