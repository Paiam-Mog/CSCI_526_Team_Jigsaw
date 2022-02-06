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

        InputHelperNonUi.onInputDown += StartDragging;
        InputHelperNonUi.onInputMove += UpdateDragging;
        InputHelperNonUi.onInputUp += StopDragging;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartDragging(InputHelperNonUi.InputData inputData)
    {
        if (collider.OverlapPoint(inputData.inputPosition)) 
        {
            dragging = true;
        }
    }

    private void UpdateDragging(InputHelperNonUi.InputData inputData)
    {
        if (!collider.OverlapPoint(inputData.inputPosition))

        {
            StopDragging(inputData);
        }

        if (dragging)
        {
            float amt = Vector3.Dot(inputData.inputPositionDeltaVector, draggableAxis);
            MoveObjectAlongAxis(amt);

        }
    }

    private void StopDragging(InputHelperNonUi.InputData inputData)
    {
        dragging = false;
    }

    private void MoveObjectAlongAxis(float amount)
    {
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
