using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{

    private bool dragging = false;
    public Vector2 draggableAxis;
    public float draggableMax;
    public float draggableMin;

    // Start is called before the first frame update
    void Start()
    {
        InputHelper.onInputDown += StartDragging;
        InputHelper.onInputMove += UpdateDragging;
        InputHelper.onInputUp += StopDragging;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartDragging(InputHelper.InputData inputData)
    {
        Debug.Log("StartDragging:" + inputData.inputPosition);

        RectTransform thisTransform = (RectTransform)transform;

        if (InputHelper.CheckInputPositionInBounds(thisTransform))
        {
            dragging = true;
        }
    }

    private void UpdateDragging(InputHelper.InputData inputData)
    {
        RectTransform thisTransform = (RectTransform)transform;
        if (!InputHelper.CheckInputPositionInBounds(thisTransform))
        {
            StopDragging(inputData);
        }

        if (dragging)
        {

        }
    }

    private void StopDragging(InputHelper.InputData inputData)
    {
        Debug.Log("StopDragging:" + inputData.inputPosition);
        dragging = false;
    }
}
