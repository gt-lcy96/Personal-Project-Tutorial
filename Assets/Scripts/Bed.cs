using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObject
{
    // Start is called before the first frame update
    public override void Pickup()
    {
        UIManager.Instance.TriggerYesNoPrompt("Do you want to sleep?", GameStateManager.Instance.Sleep);
    }
}
