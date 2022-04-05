using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWithMouse : MonoBehaviour
{
    public Vector2 draggableAxis;
    public float draggableMax;
    public float draggableMin;
    private Vector2 initialPosition;

    public MirrorInteractionCount mirrorInteractionCount;

    [SerializeField]
    private float Sensitivity = 1f;

    private float currentObjectPosition = 0;


    // Start is called before the first frame update
    void Start()
    {
        draggableAxis = draggableAxis.normalized;
        initialPosition = transform.position;
    }

    protected virtual void CheckPoint()
    {
        // return true;
    }
    
    private void StartDragging()
    {
        mirrorInteractionCount.incrementMirrorTouchCount();
    }

    public void DragForward(GameObject mirror)
    {
        StartDragging();

        currentObjectPosition += Sensitivity;
        if (currentObjectPosition > draggableMax)
        {
            currentObjectPosition = draggableMax;
        }
        if (currentObjectPosition < draggableMin)
        {
            currentObjectPosition = draggableMin;
        }
        mirror.transform.position = initialPosition + draggableAxis * currentObjectPosition;
    }

    public void DragBackward(GameObject mirror)
    {
        StartDragging();

        currentObjectPosition -= Sensitivity;
        if (currentObjectPosition > draggableMax)
        {
            currentObjectPosition = draggableMax;
        }
        if (currentObjectPosition < draggableMin)
        {
            currentObjectPosition = draggableMin;
        }
        mirror.transform.position = initialPosition + draggableAxis * currentObjectPosition;
    }

    public void PauseDrag() {
        Sensitivity = 0f;
    }
}
