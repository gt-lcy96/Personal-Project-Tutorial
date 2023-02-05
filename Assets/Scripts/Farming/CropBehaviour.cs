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

        //Set the initial state to Seed
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {

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
                break;
        }

        cropState = stateToSwitch;

    }
}