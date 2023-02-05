using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CropBehaviour : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Stages of Plant Life")]
    public GameObject seed;
    private GameObject seedling;
    private GameObject harvestable;

    //The grow points of the crop
    int growth;
    int maxGrowth;
    public enum CropState
    {
        Seed, Seedling, Harvestable
    }
    public CropState cropState;

    //Initialisation for the crop GameObject
    // called when the player plant the seed
    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        seedling = Instantiate(seedToGrow.seedling, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;
        
        harvestable = Instantiate(cropToYield.gameModel, transform);

        // game is update by minute, so convert it maxGrowth to minutes for easier calculate
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        //Set the initial state to Seed
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        growth++;

        //The seed will sprout into a seedling when the growth is at 50%
        if(growth >= maxGrowth /2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        if(growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    private void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);

        switch(stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                seedling.SetActive(true);
                break;
            case CropState.Harvestable:
                harvestable.SetActive(true);

                // unparent it before destroy the gameObject
                harvestable.transform.parent = null;
                Destroy(gameObject);
                break;
        }

        cropState = stateToSwitch;

    }
}