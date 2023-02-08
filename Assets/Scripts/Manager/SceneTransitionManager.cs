using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public enum Location { FarmLand, FarmLand_Test, MyGame }

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
    }

    public void SwitchLocation(Location locationToSwitch)
    {
        SceneManager.LoadScene(locationToSwitch.ToString());
    }
    
}
