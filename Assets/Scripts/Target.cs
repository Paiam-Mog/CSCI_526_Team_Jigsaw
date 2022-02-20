using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public UnityEvent onLevelCompleteEvent;

    [SerializeField] 
    GameManagerScript gm;

    public LaserInteractionCount laserInteractionCount;

    [SerializeField]
    private ColorState color;

    private GameObject collidedTarget;
    private bool collideWithTarget;
    private bool isCompleted;

    private void start()
    {
        collideWithTarget = false;
        isCompleted = false;
    }
    public void DetectTarget(Vector2 startPos, ColorState inputColor)
    {
         if(inputColor == color && !isCompleted)
         {
            isCompleted = true;
            OnLevelComplete();
         }     
    }

    public void OnLevelComplete()
    {
        //Debug.Log("Target Detected");
        CustomAnalytics analytics = new CustomAnalytics();
        // Debug.Log($"{topBarScript.GetLevelNumber()}, {topBarScript.GetTimer()}");
        analytics.levelCompleteTime(gm.GetLevelNumber(), gm.GetTimer());

        laserInteractionCount = FindObjectOfType<LaserInteractionCount>();

        // Debug.Log($"{topBarScript.GetLevelNumber()}, {laserInteractionCount.getLaserTouchCount()}");
        analytics.levelLaserInteractionCount(gm.GetLevelNumber(), laserInteractionCount.getLaserTouchCount());
        onLevelCompleteEvent.Invoke();
    }

}
