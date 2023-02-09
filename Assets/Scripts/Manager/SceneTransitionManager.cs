using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    
    public enum Location { FarmLand, Town, Home }
    public Location currentLocation;
    Transform playerPoint;

    private void  Awake()
    {
        //SingleTon, if there is more than 1 instance, destroy GameObject
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        //OnLocationLoad will be called when the scene is loaded
        SceneManager.sceneLoaded += OnLocationLoad;

        //Find the player's transform
        playerPoint = FindObjectOfType<PlayerController>().transform;
    }

    //Switch the player to another scene
    public void SwitchLocation(Location locationToSwitch)
    {
        SceneManager.LoadScene(locationToSwitch.ToString());
    }

    //Called when a scene is loaded    
    public void OnLocationLoad(Scene scene, LoadSceneMode mode)
    {
        //The location the player is coming from when the scene loads
        Debug.Log("currentLocation 1:  " + currentLocation);
        Location oldLocation  = currentLocation;
        Debug.Log("OnLocationLoad:  ");

        //Get the new location by converting the string of our current scene into a location enum value
        Location newLocation = (Location) Enum.Parse(typeof(Location), scene.name);
        Debug.Log("newLocation 1:  " + newLocation);

        // if the player is not coming from any location, stop the executing the function
        if (currentLocation == newLocation) return;
        //Find the start point
        Transform startPoint = LocationManager.Instance.GetPlayerStartingPosition(oldLocation);

        //Disable the players CharacterController component
        // CharacterController playerCharacter = playerPoint.GetComponent<CharacterController>();
        // playerCharacter.enabled = false;
        Debug.Log("startPoint: " + startPoint);

        //Change the player's position to the start point
        playerPoint.position = startPoint.position;
        playerPoint.rotation = startPoint.rotation;

        //Re-enable player character controller
        // playerCharacter.enabled = true;


        //Save the current location that we just switched to
        currentLocation = newLocation;
    }
}
