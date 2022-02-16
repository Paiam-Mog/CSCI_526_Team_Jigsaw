using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CustomAnalytics : MonoBehaviour
{
    // public CustomAnalytics() { }
    public void levelCompleteTime(int level, float time)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                //  TIME IN SECONDS.
                {"Level", level },
                {"Time", time }
            };
        Debug.Log("levelCompleteTime: " + level + " -> " + time);
        AnalyticsResult aa = Analytics.CustomEvent("LevelComplete", analyticsData);
        Debug.Log("Analytics Result (level complete): " + aa);
    }

    public void levelLaserInteractionCount(int level, int count)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"Level", level },
                {"Count", count }
            };
        Debug.Log("levelLaserInteractionCount: " + level + " -> " + count);
        AnalyticsResult aa = Analytics.CustomEvent("LevelLaserInteraction", analyticsData);
        Debug.Log("Analytics Result (Level v/s LaserInteraction): " + aa);
    }

}
