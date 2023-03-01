using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, ITimeTracker
{
    public static GameStateManager Instance {get; private set;}

    

    void Awake()
    {
        //Singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else {
            Instance = this;
        }

    }

    private void Start() {
        TimeManager.Instance.RegisterTracker(this);
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        //update the data as long as player is not in the farm scene
        if(SceneTransitionManager.Instance.currentLocation != SceneTransitionManager.Location.FarmLand)
        {
            //Retrieve the Land and Farm data from the static variable
            List<LandSaveState> landData = LandManager.farmData.Item1;
            List<CropSaveState> cropData = LandManager.farmData.Item2;

            //If there are no crops planted, we don't need to worry about updating anything
            if (cropData.Count == 0) return;

            for (int i = 0; i < cropData.Count; i++)
            {
                CropSaveState crop = cropData[i];
                LandSaveState land = landData[crop.landID];

                //if crop already Wilted, not need to check for clock update
                if(crop.cropState == CropBehaviour.CropState.Wilted) continue;

                //Update the land's state
                land.ClockUpdate(timestamp);
                //Update the crop state7
                if(land.landStatus ==  Land.LandStatus.watered)
                {
                    crop.Grow();
                } else if(crop.cropState != CropBehaviour.CropState.Seed)
                {
                    crop.Wither();
                }

                cropData[i] = crop;
                landData[crop.landID] = land;
            }

             LandManager.farmData.Item2.ForEach((CropSaveState crop) => {
                Debug.Log(crop.seedToGrow + "\n Health: " + crop.health + "\n Growth: " + crop.growth + "\n State: " + crop.cropState.ToString());
            });
        }
    }

    public void Sleep()
    {
        Debug.Log("Sleep");
    }
}
