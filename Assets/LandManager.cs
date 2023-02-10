using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager Instance {get; private set; }

    List<Land> landPlots = new List<Land>();
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
                land.id = landPlots.Count - 1;
            }
        }
    }



    void Start()
    {
        RegisterPlots();
        
        // Debug.Log("landPlots.Count: " + landPlots.Count);
    }

    void Update()
    {
        Debug.Log(landPlots.Count);
    }
}
