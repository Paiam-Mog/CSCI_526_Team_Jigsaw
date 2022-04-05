using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteractionCount : MonoBehaviour {
    private int laserTouchCount;

    void Start() {
        laserTouchCount = 0;
    }


    public int getLaserTouchCount() {
        return laserTouchCount;
    }

    public void setLaserTouchCount(int count) {
        laserTouchCount = count;
    }

    public void incrementLaserTouchCount() {
        laserTouchCount++;
    }


}