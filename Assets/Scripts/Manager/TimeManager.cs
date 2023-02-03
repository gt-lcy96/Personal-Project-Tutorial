using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    // SingleTon
    public static TimeManager Instance { get; private set; }

    [SerializeField]
    GameTimestamp timestamp;
    public float timescale = 1;

    public Transform sunTransform;

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

    void Start() {
        timestamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }


    IEnumerator TimeUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1/timescale);;
            Tick();
        }

    }

    // A tick of the in-game time
    public void Tick()
    {
        timestamp.UpdateClock();

        // Convert current time to minute
        int timeToMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        // Sun moves 15 degree in an hour
        // .255 degree in a minute
        // At midnight (0.00), the angle of the sun should be -90
        float sunAngle = .25f * timeToMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }
}