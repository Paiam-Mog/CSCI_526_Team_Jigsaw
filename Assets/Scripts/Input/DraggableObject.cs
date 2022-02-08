using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{

    private bool dragging = false;
    public Vector2 draggableAxis;
    public float draggableMax;
    public float draggableMin;
    private float currentObjectPosition = 0;
    private Vector2 initialPosition;


    // Start is called before the first frame update
    void Start() {
        draggableAxis = draggableAxis.normalized;
        initialPosition = transform.position;
        InputHelper.onInputDown += StartDragging;
        InputHelper.onInputMove += UpdateDragging;
        InputHelper.onInputUp += StopDragging;
    }

    // Update is called once per frame
    void Update() {

    }

    private void StartDragging(InputHelper.InputData inputData) {
        //Debug.Log("StartDragging:" + inputData.inputPosition);

        if (CheckPoint()) {
            dragging = true;
        }
    }

    private void UpdateDragging(InputHelper.InputData inputData) {
        if (!CheckPoint()) {
            StopDragging(inputData);
        }

        if (dragging) {
            float amt = Vector3.Dot(InputMoveDelta(inputData), draggableAxis);
            MoveObjectAlongAxis(amt);

        }
    }

    private void StopDragging(InputHelper.InputData inputData) {
        //Debug.Log("StopDragging:" + inputData.inputPosition);
        dragging = false;
    }

    private void MoveObjectAlongAxis(float amount) {
        currentObjectPosition += amount;
        if (currentObjectPosition > draggableMax) {
            currentObjectPosition = draggableMax;
        }
        if (currentObjectPosition < draggableMin) {
            currentObjectPosition = draggableMin;
        }
        transform.position = initialPosition + draggableAxis * currentObjectPosition;
    }

    protected virtual bool CheckPoint() {
        RectTransform thisTransform = (RectTransform)transform;

        return InputHelper.CheckInputPositionInBounds(thisTransform);
    }

    protected virtual Vector2 InputMoveDelta(InputHelper.InputData inputData) {
        return inputData.inputPositionDeltaVector;

    }

}
