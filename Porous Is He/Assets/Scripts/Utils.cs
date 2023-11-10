using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class Utils
{
    /// <summary>
    /// Given a min1, max1, and value1, this returns a float value which represents 
    /// a value in between min2 and max2.And this value is proportional to where
    /// value1 is between min1 and max1.
    /// </summary>
    public static float ConvertRatio(float min1, float max1, float value1, float min2, float max2)
    {
        float totalDiff = max1 - min1;
        float valueDiff = value1 - min1;
        float percentage = valueDiff / totalDiff;

        return min2 + ((max2 - min2) * percentage);
    }
}
