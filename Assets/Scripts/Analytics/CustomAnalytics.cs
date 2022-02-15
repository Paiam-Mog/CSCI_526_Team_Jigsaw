using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CustomAnalytics : MonoBehaviour
{
    public CustomAnalytics() { }
    public void levelCompleteTime(int level, float time)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                //  TIME IN SECONDS.
                {"Level", level },
                {"Time", time }
            };

        AnalyticsResult aa = Analytics.CustomEvent("LevelComplete", analyticsData);

        Debug.Log("Analytics Result (level complete): " + aa);
    }

}
