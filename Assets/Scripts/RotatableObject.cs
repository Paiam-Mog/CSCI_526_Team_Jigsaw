using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public struct Quadrant
	{

	}

    private bool rotating = false;

    // Start is called before the first frame update
    void Start()
    {
        InputHelper.onInputDown += StartRotating;
        InputHelper.onInputMove += UpdateRotating;
        InputHelper.onInputUp += StopRotating;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartRotating(InputHelper.InputData inputData) {
        RectTransform thisTransform = (RectTransform)transform;

        if (InputHelper.CheckInputPositionInBounds(thisTransform)) {
            rotating = true;
        }
	}

    private void UpdateRotating(InputHelper.InputData inputData) {

	}

    private void StopRotating(InputHelper.InputData inputData) {
        rotating = false;
	}
}
