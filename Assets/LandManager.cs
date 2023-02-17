using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager Instance {get; private set; }
    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    List<Land> landPlots = new List<Land>();

    List<LandSaveState> landData = new List<LandSaveState>();
    List<CropSaveState> cropData = new List<CropSaveState>();

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
#region Registering and Deregistering
    void RegisterPlots()
    {
        //Loop through all the column of Land under parent
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform columnChild = transform.GetChild(i);

            // loop through the children of transform to get each Land component
            foreach(Transform landTransform in columnChild.transform)
            {
                Land land = landTransform.GetComponent<Land>();
                landPlots.Add(land);

                //Create a corresponding LandSaveState
                landData.Add(new LandSaveState());

                land.id = landPlots.Count - 1;
            }
        }
    }

    public void RegisterCrop(int landID, SeedData seedToGrow, CropBehaviour.CropState cropState, int growth, int health)
    {
        cropData.Add(new CropSaveState(landID, seedToGrow.name, cropState, growth, health));
    }

    public void DeregisterCrop(int landID)
    {
        cropData.RemoveAll(x => x.landID == landID);
    }
#endregion

#region State Changes
    //Update the corresponding Land Data on ever change to the Land's state
    public void OnLandStateChange(int id, Land.LandStatus landStatus, GameTimestamp lastWatered)
    {
        landData[id] = new LandSaveState(landStatus, lastWatered);
    }


    //Update the corrresponding Crop Data on ever change to the Land's state
    public void OnCropStateChange(int landID, CropBehaviour.CropState cropState, int growth, int health)
    {
        int cropIndex = cropData.FindIndex(x => x.landID == landID);
        string seedToGrow = cropData[cropIndex].seedToGrow;
        cropData[cropIndex] = new CropSaveState(landID, seedToGrow, cropState, growth, health);
    }
#endregion
    
#region Load Data
    public void ImportLandData(List<LandSaveState> landDatasetToLoad)
    {
        for(int i=0; i < landDatasetToLoad.Count; i++)
        {   
            //Load the individhual land save state
            LandSaveState landDataToLoad =  landDatasetToLoad[i];
            landPlots[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWatered);
        }

        landData = landDatasetToLoad;
    }

    public void ImportCropData(List<CropSaveState> cropDatasetToLoad)
    {
        cropData = cropDatasetToLoad;
        foreach (CropSaveState cropSave in cropDatasetToLoad)
        {
            Land landToPlant = landPlots[cropSave.landID];

            CropBehaviour cropToPlant = landToPlant.SpawnCrop();

            SeedData seedToGrow = (SeedData) InventoryManager.Instance.itemIndex.GetItemFromString(cropSave.seedToGrow);
            cropToPlant.LoadCrop(cropSave.landID, seedToGrow, cropSave.cropState, cropSave.growth, cropSave.health);
        }
    }
#endregion
    private void OnDestroy()
    {
        // save the farmData whenever player leave the scene
        farmData = new Tuple<List<LandSaveState>, List<CropSaveState>>(landData, cropData);
    }

    void Start()
    {
        RegisterPlots();
        
        StartCoroutine(LoadFarmData());
    }

    IEnumerator LoadFarmData()
    {
        yield return new WaitForEndOfFrame();
        if(farmData != null)
        {
            ImportLandData(farmData.Item1);
            ImportCropData(farmData.Item2);
        }
    }
    
}
