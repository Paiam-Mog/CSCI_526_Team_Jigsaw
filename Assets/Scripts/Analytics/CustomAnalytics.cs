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
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelComplete", analyticsData);
        Debug.Log("Analytics Result (level complete): " + analyticsResult);
    }

    public void levelLaserInteractionCount(int level, int count)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"Level", level },
                {"Count", count }
            };
        Debug.Log("levelLaserInteractionCount: " + level + " -> " + count);
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelLaserInteraction", analyticsData);
        Debug.Log("Analytics Result (Level v/s LaserInteraction): " + analyticsResult);
    }

    public void levelCompleteStars(int level, int stars)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"Level", level },
                {"Stars", stars }
            };
        Debug.Log("levelCompleteStars: " + level + " -> " + stars);
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelCompleteStars", analyticsData);
        Debug.Log("Analytics Result (level complete stars): " + analyticsResult);
    }

    public void levelStartedVsFinished(int level, int started, int finished)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"Level", level },
                {"Started", started },
                {"Finished", finished}
            };
        Debug.Log("levelStartedVsFinished: " + level + " -> " + started + " -> " + finished);
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelStartedFinished", analyticsData);
        Debug.Log("Analytics Result (level started vs finsihed): " + analyticsResult);
    }

    public void levelMirrorInteractionCount(int level, int count)
    {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
            {
                {"Level", level },
                {"mirrorInteractionCount", mirrorInteractionCount },
            };
        Debug.Log("levelMirrorInteractionCount " + level + " -> " + mirrorInteractionCount);
        AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelMirrorInteraction", analyticsData);
        Debug.Log("Analytics Result (level v/s mirror interaction): " + analyticsResult);
    }

}
