using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance { get; private set;}

    public List<StartPoint> startPoints;
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

    //Find the player's start position based on where he come from
    public Transform GetPlayerStartingPosition(SceneTransitionManager.Location enteringFrom)
    {
        //Tries to find the matching startpoint based on the location given
        StartPoint startingPoint = startPoints.Find(x => x.enteringFrom == enteringFrom);
        return startingPoint.playerStart;
    }
}
