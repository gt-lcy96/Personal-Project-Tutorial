using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemData itemToDisplay;
    public Image itemDisplayImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(ItemData itemToDisplay)
    {
        if(itemToDisplay != null)
        {
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);

            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
    }


}