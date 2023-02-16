using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CropBehaviour : MonoBehaviour
{
    int landID;  
    SeedData seedToGrow;

    [Header("Stages of Plant Life")]
    public GameObject seed;
    public GameObject wilted;
    private GameObject seedling;
    private GameObject harvestable;

    //The grow points of the crop
    int growth;
    int maxGrowth;

    // The Crop can stay alive for 48 hours wihtout water before it dies
    int maxHealth = GameTimestamp.HoursToMinutes(40);
    int health;
    public enum CropState
    {
        Seed, Seedling, Harvestable, Wilted
    }
    public CropState cropState;

    //Initialisation for the crop GameObject
    // called when the player plant the seed
    public void Plant(int landID, SeedData seedToGrow)
    {
        LoadCrop(landID, seedToGrow, CropState.Seed, 0, 0);
        LandManager.Instance.RegisterCrop(landID, seedToGrow, cropState, growth, health);
    }

    public void LoadCrop(int landID, SeedData seedToGrow, CropState cropState, int growth, int health)
    {
        this.landID = landID;
        this.seedToGrow = seedToGrow;

        seedling = Instantiate(seedToGrow.seedling, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;
        
        harvestable = Instantiate(cropToYield.gameModel, transform);

        // game is update by minute, so convert it maxGrowth to minutes for easier calculate
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        this.growth = growth;
        this.health = health;

        if(seedToGrow.regrowable)
        {
            RegrowableHarvestBehaviour regrowableHarvest = harvestable.GetComponent<RegrowableHarvestBehaviour>();
            regrowableHarvest.SetParent(this);
        }

        //Set the initial state to Seed
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        growth++;

        // Restore the health of the plant when it is watered
        if(health < maxHealth)
        {
            health ++;
        }

        //The seed will sprout into a seedling when the growth is at 50%
        if(growth >= maxGrowth /2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //Grow from seedling to harvestable
        if(growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }

        LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);
    }

    //The crop will progressively wither when the soil is dry
    public void Wither()
    {
        health--;

        if(health <= 0 && cropState != CropState.Seed)
        {
            SwitchState(CropState.Wilted);
        }

        LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);
    }

    private void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);
        wilted.SetActive(false);

        switch(stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                seedling.SetActive(true);
                health = maxHealth;
                break;
            case CropState.Harvestable:
                harvestable.SetActive(true);

                // If the seed is not regrowable, detach the harvestable from this corp gameobject and destroy it
                if(!seedToGrow.regrowable)
                {
                    // unparent it before destroy the gameObject
                    harvestable.transform.parent = null;
                    Destroy(gameObject);
                }
                    break;
            case CropState.Wilted:
                wilted.SetActive(true);
                break;
        }

        cropState = stateToSwitch;

    }

    //Called when the player harvest a regrowable crop. Resets the state to seedling
    public void Regrow()
    {
        //Reset growth
        //Get the regrowth time in hours
        int hoursToReGrow = GameTimestamp.DaysToHours(seedToGrow.daysToRegrow);
        growth = maxGrowth - GameTimestamp.HoursToMinutes(hoursToReGrow);

        SwitchState(CropState.Seedling);
    }

    void OnDestroy()
    {
        LandManager.Instance.DeregisterCrop(landID);
    }
}