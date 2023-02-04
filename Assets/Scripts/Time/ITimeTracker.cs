using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITimeTracker
{
    void ClockUpdate(GameTimestamp timestamp);
}