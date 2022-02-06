using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjectNonUi : MonoBehaviour
{
    public Vector2 draggableAxis;
    public float draggableMax;
    public float draggableMin;

    private bool dragging = false;
    private float currentObjectPosition = 0;
    private Vector2 initialPosition;
    Collider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        draggableAxis = draggableAxis.normalized;
        initialPosition = this.transform.position;
        collider = GetComponent<Collider2D>();

        Debug.Log("working");

        InputHelper_NonUI.onInputDown += StartDragging;
        InputHelper_NonUI.onInputMove += UpdateDragging;
        InputHelper_NonUI.onInputUp += StopDragging;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartDragging(InputHelper_NonUI.InputData inputData)
    {
        Vector3 wp = inputData.inputPosition;
        Debug.Log("start dragging:" + wp);
        if (collider.OverlapPoint(wp)) 
        {
            dragging = true;
        }
    }

    private void UpdateDragging(InputHelper_NonUI.InputData inputData)
    {
        Vector3 wp = inputData.inputPosition;

        if (!collider.OverlapPoint(wp))
        {
            StopDragging(inputData);
        }

        if (dragging)
        {
            float amt = Vector3.Dot(inputData.inputPositionDeltaVector, draggableAxis);
            MoveObjectAlongAxis(amt);

        }
    }

    private void StopDragging(InputHelper_NonUI.InputData inputData)
    {
        Debug.Log("StopDragging:" + inputData.inputPosition);
        dragging = false;
    }

    private void MoveObjectAlongAxis(float amount)
    {
        Debug.Log("Amount" + amount);
        
        currentObjectPosition += amount;
        if (currentObjectPosition > draggableMax)
        {
            currentObjectPosition = draggableMax;
        }
        if (currentObjectPosition < draggableMin)
        {
            currentObjectPosition = draggableMin;
        }
        this.transform.position = initialPosition + draggableAxis * currentObjectPosition;
    }
}
