using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteractionCount : MonoBehaviour {
    private int laserTouchCount;

    void Start() {
        laserTouchCount = 0;
    }


    public int getLaserTouchCount() {
        Debug.Log(laserTouchCount + " in getLaserTouchCount");
        return laserTouchCount;
    }

    public void setLaserTouchCount(int count) {
        Debug.Log(count + " in setLaserTouchCount");
        laserTouchCount = count;
    }

    public void incrementLaserTouchCount() {
        laserTouchCount++;
        Debug.Log(laserTouchCount + " in incrementLaserTouchCount");
    }


}