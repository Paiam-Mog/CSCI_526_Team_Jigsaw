using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent onLevelCompleteEvent;

    public TopBarScript topBarScript;

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
        //Debug.Log($"{topBarScript.GetLevelNumber()}, {topBarScript.GetTimer()}");
        analytics.levelCompleteTime(topBarScript.GetLevelNumber(), topBarScript.GetTimer());
        onLevelCompleteEvent.Invoke();
    }

}
