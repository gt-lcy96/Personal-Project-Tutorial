using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEntryPoint : MonoBehaviour
{
    [SerializeField]
    SceneTransitionManager.Location locationToSwitch;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.SwitchLocation(locationToSwitch);
        }
    }
}
