using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public enum RotationStyle
    {
        FreeRotate,
        SnapToDegreeSegment,
        MinAndMax
    }

    public LaserInteractionCount laserInteractionCount;

    [Header("Rotation Variables")]
    public RotationStyle rotationBlockMethod;
    public float Sensitivity = 0.2f;

    [Header("Snap To Degree Segment Variables")]
    public int degreeSegment = 15;

    [Header("Min and Max Variables")]
    public float angleMax;
    public float angleMin;

    private bool rotating = false;
    private float currentAngle = 0f;
    private float currentSegmentAngle = 0f;
    private float angleRotated = 0f;

    private Vector3 startDragDir;
    private Vector3 currentDragDir;
    private Quaternion initialRotation;
    private float angleFromStart;

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

            Debug.Log("StartRotating");
            laserInteractionCount.incrementLaserTouchCount();

            // Vector2 toMouse =inputData.inputPosition - (Vector2)transform.position;
           Vector2 toMouse = (Vector2)Camera.main.ScreenToWorldPoint(inputData.inputPosition) - (Vector2)transform.position;
           currentAngle = Vector3.SignedAngle(Vector2.up, toMouse.normalized, Vector3.forward);
           currentSegmentAngle = Vector3.SignedAngle(Vector2.up, transform.up, transform.forward);

            
            startDragDir = Camera.main.ScreenToWorldPoint(inputData.inputPosition) - transform.position;

            initialRotation = transform.rotation;

            angleRotated = 0f;
        }
    }

    private void UpdateRotating(InputHelper.InputData inputData)
    {
        if (rotating)
        {
            // Vector2 toMouse = inputData.inputPosition - (Vector2)transform.position;
            Vector2 toMouse = (Vector2)Camera.main.ScreenToWorldPoint(inputData.inputPosition)- (Vector2)transform.position;

            Vector3 difference = Camera.main.ScreenToWorldPoint(inputData.inputPosition) - transform.position;


            float newAngle = Vector3.SignedAngle(Vector2.up, toMouse.normalized, Vector3.forward);
            //Debug.Log("currentAngle:" + newAngle);
            // float newAngle = Vector3.SignedAngle(toMouse, toMouse.normalized, Vector3.forward);

            //if (newAngle < 0) {
            //    newAngle = Mathf.Abs(newAngle) - 180f;
            //}

            switch (rotationBlockMethod)
            {
                case RotationStyle.FreeRotate:

                    difference.Normalize();


                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                   // transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - (90 + offset));


                    //transform.up = Quaternion.AngleAxis((newAngle - currentAngle) * Sensitivity, Vector3.forward) * transform.up;
                    // transform.RotateAround(transform.position, transform.forward, (newAngle - currentAngle) * Sensitivity);
                    break;
                case RotationStyle.SnapToDegreeSegment:
                    angleRotated += newAngle - currentAngle;

                    if (angleRotated >= degreeSegment)
                    {
                        currentSegmentAngle += degreeSegment;
                        angleRotated = 0f;
                    }
                    else if (angleRotated <= -degreeSegment)
                    {
                        currentSegmentAngle -= degreeSegment;
                        angleRotated = 0f;
                    }
                    transform.up = Quaternion.AngleAxis(currentSegmentAngle, Vector3.forward) * Vector3.up;
                    break;
                case RotationStyle.MinAndMax:
                    currentSegmentAngle += newAngle - currentAngle;

                    if (currentSegmentAngle > angleMax)
                    {
                        transform.up = Quaternion.AngleAxis(angleMax, Vector3.forward) * Vector3.up;
                    }
                    else if (currentSegmentAngle < angleMin)
                    {
                        transform.up = Quaternion.AngleAxis(angleMin, Vector3.forward) * Vector3.up;
                    }
                    else
                    {
                        transform.up = Quaternion.AngleAxis(currentSegmentAngle, Vector3.forward) * Vector3.up;
                    }
                    break;
            }

            currentAngle = newAngle;
        }
    }

    private void StopRotating(InputHelper.InputData inputData)
    {
        rotating = false;
        currentAngle = 0f;
        currentSegmentAngle = 0f;
    }

    protected virtual bool CheckPoint()
    {
        RectTransform thisTransform = (RectTransform)transform;

        return InputHelper.CheckInputPositionInBounds(thisTransform);
    }
}
