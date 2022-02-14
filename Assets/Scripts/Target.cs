using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

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
        else
        {
            Debug.Log("Target Color not matched");

        }
        
    }

    public void OnLevelComplete()
    {

        Debug.Log("OnLevelComplete");
    }

}
