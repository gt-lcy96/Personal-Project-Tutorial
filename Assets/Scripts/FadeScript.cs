using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup uIgroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public void ShowUI()
    {

        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }


    private void Update()
    {
        if (fadeIn)
        {
            if(uIgroup.alpha < 1)
            {
                uIgroup.alpha += Time.deltaTime;
                if(uIgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if(uIgroup.alpha >= 0)
            {
                uIgroup.alpha -= Time.deltaTime;
                if(uIgroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
