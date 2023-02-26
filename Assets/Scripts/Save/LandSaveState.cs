using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public GameTimestamp lastWatered;

    public LandSaveState(Land.LandStatus landStatus, GameTimestamp lastWatered)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatered;
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if(landStatus == Land.LandStatus.watered)
        {
            // Hours since the land was watered
            int hourElapsed = GameTimestamp.CompareTimestamps(lastWatered, timestamp);            

            if(hourElapsed > 24)
            {
                //Dry up (Switch Back to tilledLand)
                landStatus = Land.LandStatus.tilledLand;
            }
        }

       
    }

}
