using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    // SingleTon
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField]
    GameTimestamp timestamp;
    public float timescale = 1;

    [Header("Day and Night cylce")]
    public Transform sunTransform;

    // List of Objects to inform of changes to the time
    List<ITimeTracker> listeners = new List<ITimeTracker>();
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
            Tick();
            yield return new WaitForSeconds(1/timescale);;
        }

    }

    // A tick of the in-game time
    public void Tick()
    {
        
        timestamp.UpdateClock();
        foreach (ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }

        UpdateSumMovement();
    }

    void UpdateSumMovement()
    {
        // Convert current time to minute
        int timeToMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        // Sun moves 15 degree in an hour
        // .255 degree in a minute
        // At midnight (0.00), the angle of the sun should be -90
        float sunAngle = .25f * timeToMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    public GameTimestamp GetGameTimestamp()
    {
        // Return a cloned timestamp
        return new GameTimestamp(timestamp);
    }

    // Handling Listerner
    
    // Add the object to the list of listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }


}