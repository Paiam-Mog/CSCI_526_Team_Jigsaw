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
    // public MirrorInteractionCount mirrorInteractionCount;

    [SerializeField]
    private ColorState color;

    public SpriteRenderer targetNodeSprite;
    public ColorTable colorTable;

    private GameObject collidedTarget;
    private bool collideWithTarget;
    private bool isCompleted;

    private void start()
    {
        collideWithTarget = false;
        isCompleted = false;

        if (targetNodeSprite != null && colorTable != null)
        {
            targetNodeSprite.color = colorTable.GetColor(color);
        }
        
        CustomAnalytics customAnalytics = new CustomAnalytics();
        customAnalytics.levelStartedVsFinished(gm.GetLevelNumber(), 1, 0);
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
        CustomAnalytics customAnalytics = new CustomAnalytics();
        customAnalytics.levelCompleteTime(gm.GetLevelNumber(), gm.GetTimer());

        laserInteractionCount = FindObjectOfType<LaserInteractionCount>();
        customAnalytics.levelLaserInteractionCount(gm.GetLevelNumber(), laserInteractionCount.getLaserTouchCount());

        customAnalytics.levelCompleteStars(gm.GetLevelNumber(), gm.GetStarCount());
        customAnalytics.levelStartedVsFinished(gm.GetLevelNumber(), 0, 1);

        // mirrorInteractionCount = FindObjectOfType<MirrorInteractionCount>();
        // customAnalytics.levelMirrorInteractionCount(gm.GetLevelNumber(), mirrorInteractionCount.getMirrorTouchCount());

        onLevelCompleteEvent.Invoke();
    }

}
