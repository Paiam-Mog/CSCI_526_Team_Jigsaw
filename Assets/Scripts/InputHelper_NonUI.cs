using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHelper_NonUI : MonoBehaviour
{
    public enum InputState
    {
        InputDown,
        InputMove,
        InputUp,
        None
    }

    [System.Serializable]
    public struct InputData
    {
        public Vector3 inputPosition;
        public Vector3 inputPositionDeltaVector;
        public Vector3 inputPositionDeltaUnitVector;
        public InputState inputState;
    }

    public UnityEvent<InputData> OnInputDownEvent;
    public UnityEvent<InputData> OnInputMoveEvent;
    public UnityEvent<InputData> OnInputUpEvent;

    public static UnityAction<InputData> onInputDown;
    public static UnityAction<InputData> onInputMove;
    public static UnityAction<InputData> onInputUp;

    private static InputData currentInputData;
    private Vector3 previousPosition;
   // private Vector3 previousPosition1;

    // Start is called before the first frame update
    void Start()
    {
        onInputDown += OnInputDownEvent.Invoke;
        onInputMove += OnInputMoveEvent.Invoke;
        onInputUp += OnInputUpEvent.Invoke;

        currentInputData.inputPosition = Vector2.zero;
        currentInputData.inputPositionDeltaVector = Vector2.zero;
        currentInputData.inputPositionDeltaUnitVector = Vector2.zero;
        currentInputData.inputState = InputState.None;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // No input recently
            if (currentInputData.inputState == InputState.None)
            {
                currentInputData.inputPosition = touchPosition;
                currentInputData.inputPositionDeltaVector = Vector3.zero;
                currentInputData.inputPositionDeltaUnitVector = Vector3.zero;
                currentInputData.inputState = InputState.InputDown;

                onInputDown.Invoke(currentInputData);
            }
            else if (currentInputData.inputState == InputState.InputDown)
            {
                 Vector3 delta = touchPosition - previousPosition;

                currentInputData.inputPosition = touch.position;
                currentInputData.inputPositionDeltaVector = delta;
                currentInputData.inputPositionDeltaUnitVector = delta.normalized;
                currentInputData.inputState = InputState.InputMove;

                onInputMove.Invoke(currentInputData);
            }
            else if (currentInputData.inputState == InputState.InputMove)
            {
                Vector3 delta = touchPosition - previousPosition;
                currentInputData.inputPositionDeltaVector = delta;
                currentInputData.inputPositionDeltaUnitVector = delta.normalized;

                onInputMove.Invoke(currentInputData);
            }

            previousPosition = touchPosition;
        }
        else if (Input.touchCount == 0)
        {
            if (currentInputData.inputState == InputState.InputMove || currentInputData.inputState == InputState.InputDown)
            {
                currentInputData.inputState = InputState.InputUp;

                OnInputUpEvent.Invoke(currentInputData);
            }
            else if (currentInputData.inputState == InputState.InputUp)
            {
                currentInputData.inputPositionDeltaVector = Vector2.zero;
                currentInputData.inputPositionDeltaUnitVector = Vector2.zero;
                currentInputData.inputState = InputState.None;
            }
        }

        // Handle Mouse Input
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            currentInputData.inputPosition = position;
            currentInputData.inputPositionDeltaVector = Vector2.zero;
            currentInputData.inputPositionDeltaUnitVector = Vector2.zero;
            currentInputData.inputState = InputState.InputDown;

            onInputDown.Invoke(currentInputData);
            previousPosition = position;

            //Debug.Log("Mouse Input Down");
            //DisplayInputData();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = position - previousPosition;

            currentInputData.inputPosition = position;
            currentInputData.inputPositionDeltaVector = delta;
            currentInputData.inputPositionDeltaUnitVector = delta.normalized;
            currentInputData.inputState = InputState.InputMove;

            onInputMove.Invoke(currentInputData);
            previousPosition = position;

            //Debug.Log("Mouse Input Move");
            //DisplayInputData();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 delta = position - previousPosition;

            currentInputData.inputPosition = position;
            currentInputData.inputPositionDeltaVector = delta;
            currentInputData.inputPositionDeltaUnitVector = delta.normalized;
            currentInputData.inputState = InputState.InputUp;

            onInputUp.Invoke(currentInputData);
            previousPosition = position;

            //Debug.Log("Mouse Input Up");
            //DisplayInputData();
        }
        else
        {
            Vector3 delta = position - previousPosition;

            currentInputData.inputPosition = position;
            currentInputData.inputPositionDeltaVector = delta;
            currentInputData.inputPositionDeltaUnitVector = delta.normalized;
            currentInputData.inputState = InputState.None;

            //Debug.Log("Mouse Input None");
            //DisplayInputData();
        }
    }

    /// <summary>
    /// Returns true if the input position is within the bounds of a rect transform
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns></returns>
    public static bool CheckInputPositionInBounds(RectTransform rectTransform)
    {
        Vector2 localInputPosition = rectTransform.InverseTransformPoint(currentInputData.inputPosition);

        return rectTransform.rect.Contains(localInputPosition);
    }

    public static InputData GetInputData()
    {
        return currentInputData;
    }

    private void DisplayInputData()
    {
        Debug.Log(
            "Input Position: " + currentInputData.inputPosition +
            "\nPosition Delta Vector: <" + currentInputData.inputPositionDeltaVector.x + ", " + currentInputData.inputPositionDeltaVector.y + ">" +
            "\nPosition Delta Unit Vector: <" + currentInputData.inputPositionDeltaUnitVector.x + ", " + currentInputData.inputPositionDeltaUnitVector.y + ">" +
            "\nInput State: " + currentInputData.inputState
        );
    }
}
