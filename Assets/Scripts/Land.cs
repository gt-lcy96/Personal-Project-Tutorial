using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum LandStatus
    {
        Dirt, tilledLand, watered
    }

    [SerializeField]
    private Material dirtMat, tilledLandMat, wateredMat;
    new Renderer renderer;
    public LandStatus landStatus;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        //set default material for land status
        SwitchLandStatus(LandStatus.Dirt);
    }

    // Update is called once per frame
    public void SwitchLandStatus(LandStatus status)
    {
        landStatus = status;
        Material material = dirtMat;
        switch(status)
        {
            case LandStatus.Dirt:
                material = dirtMat;
                break;
            case LandStatus.tilledLand:
                material = tilledLandMat;
                break;
            case LandStatus.watered:
                material = wateredMat;
                break;
        }
    }
}
