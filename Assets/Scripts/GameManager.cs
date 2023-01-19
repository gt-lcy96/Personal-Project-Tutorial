using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public int killCount;
    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        killText.text = "kill : " + killCount;
    }
}
