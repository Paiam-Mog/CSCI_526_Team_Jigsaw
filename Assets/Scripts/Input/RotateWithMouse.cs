using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    private bool rotating = false;
    public LaserInteractionCount laserInteractionCount;
    public MirrorInteractionCount mirrorInteractionCount;

    public bool isLaserCannon = true;

    [SerializeField]
    private float Sensitivity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // laserInteractionCount = new LaserInteractionCount();
        // InputHelper.onInputDown += StartRotating;
    }

    private void StartRotating()
    {
        if (isLaserCannon)
            laserInteractionCount.incrementLaserTouchCount();
        else
            mirrorInteractionCount.incrementMirrorTouchCount();
    
    }

    public void RotateClockwise(GameObject cannon){
        StartRotating();

        // Debug.Log(cannon.transform.position);
        cannon.transform.Rotate(0f, 0f, -1f * Sensitivity);
    }

    public void RotateCounterClockwise(GameObject cannon){
        StartRotating();

        // Debug.Log(cannon.transform.position);
        cannon.transform.Rotate(0f, 0f, 1f * Sensitivity);
    }


    protected virtual void CheckPoint()
    {
        // return true;

    }

    public void PauseRotation() {
        Sensitivity = 0f;
	}
}
