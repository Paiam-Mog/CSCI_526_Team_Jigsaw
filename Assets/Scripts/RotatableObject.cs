using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public enum RotationStyle
	{
        FreeRotate,
        SnapToDegreeSegment,
        MinAndMax
	}

    [Header("Rotation Variables")]
    public RotationStyle rotationBlockMethod;
    
    [Header("Snap To Degree Segment Variables")]
    public int degreeSegment = 15;

    [Header("Min and Max Variables")]
    public float angleMax;
    public float angleMin;

    private bool rotating = false;
    private float currentAngle = 0f;
    private float currentSegmentAngle = 0f;
    private float angleRotated = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InputHelper.onInputDown += StartRotating;
        InputHelper.onInputMove += UpdateRotating;
        InputHelper.onInputUp += StopRotating;
    }

    private void StartRotating(InputHelper.InputData inputData) {
        RectTransform thisTransform = (RectTransform)transform;

       if (InputHelper.CheckInputPositionInBounds(thisTransform)) {
            rotating = true;
            Vector2 toMouse = inputData.inputPosition - (Vector2)thisTransform.position;
            currentAngle = Vector3.SignedAngle(Vector2.up, toMouse.normalized, Vector3.forward);

            currentSegmentAngle = Vector3.SignedAngle(Vector2.up, transform.up, transform.forward);
            angleRotated = 0f;
        }
	}

    private void UpdateRotating(InputHelper.InputData inputData) {
        if (rotating) {
            Vector2 toMouse = inputData.inputPosition - (Vector2)transform.position;
            float newAngle = Vector3.SignedAngle(Vector2.up, toMouse.normalized, Vector3.forward);

            switch(rotationBlockMethod) {
                case RotationStyle.FreeRotate:
                    transform.up = Quaternion.AngleAxis(newAngle - currentAngle, Vector3.forward) * transform.up;
                    break;
                case RotationStyle.SnapToDegreeSegment:
                    angleRotated += newAngle - currentAngle;

                    if (angleRotated >= degreeSegment) {
                        currentSegmentAngle += degreeSegment;
                        angleRotated = 0f;
                    }
                    else if (angleRotated <= -degreeSegment) {
                        currentSegmentAngle -= degreeSegment;
                        angleRotated = 0f;
					}
                    transform.up = Quaternion.AngleAxis(currentSegmentAngle, Vector3.forward) * Vector3.up;
                    break;
                case RotationStyle.MinAndMax:
                    currentSegmentAngle += newAngle - currentAngle;

                    if (currentSegmentAngle > angleMax) {
                        transform.up = Quaternion.AngleAxis(angleMax, Vector3.forward) * Vector3.up;
					}
                    else if (currentSegmentAngle < angleMin) {
                        transform.up = Quaternion.AngleAxis(angleMin, Vector3.forward) * Vector3.up;
					}
                    else {
                        transform.up = Quaternion.AngleAxis(currentSegmentAngle, Vector3.forward) * Vector3.up;
                    }
                    break;
			}            

            currentAngle = newAngle;
        }
    }

    private void StopRotating(InputHelper.InputData inputData) {
        rotating = false;
        currentAngle = 0f;
        currentSegmentAngle = 0f;
	}
}
