using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent onLevelCompleteEvent;

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
        onLevelCompleteEvent.Invoke();
    }

}
