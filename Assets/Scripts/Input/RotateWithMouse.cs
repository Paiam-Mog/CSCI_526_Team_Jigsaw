using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    private bool rotating = false;
    public LaserInteractionCount laserInteractionCount;

    // Start is called before the first frame update
    void Start()
    {
        // laserInteractionCount = new LaserInteractionCount();
        InputHelper.onInputDown += StartRotating;
        InputHelper.onInputMove += UpdateRotating;
        InputHelper.onInputUp += StopRotating;
    }

    private void StartRotating(InputHelper.InputData inputData)
    {
        if (CheckPoint())
        {
            rotating = true;
            laserInteractionCount.incrementLaserTouchCount();
        }
    }

    private void UpdateRotating(InputHelper.InputData inputData)
    {
        if (rotating)
        {
            Vector2 toMouse = (Vector2)Camera.main.ScreenToWorldPoint(inputData.inputPosition)- (Vector2)transform.position;
            Vector3 difference = Camera.main.ScreenToWorldPoint(inputData.inputPosition) - transform.position;

            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
               
        }
    }

    private void StopRotating(InputHelper.InputData inputData)
    {
        rotating = false;
    }

    protected virtual bool CheckPoint()
    {
        RectTransform thisTransform = (RectTransform)transform;

        return InputHelper.CheckInputPositionInBounds(thisTransform);
    }
}
