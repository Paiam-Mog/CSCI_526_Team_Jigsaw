using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteractionCount : MonoBehaviour {
    private int mirrorTouchCount;

    void Start() {
        mirrorTouchCount = 0;
    }


    public int getMirrorTouchCount() {
        Debug.Log(mirrorTouchCount + " in getMirrorTouchCount");
        return mirrorTouchCount;
    }

    public void setMirrorTouchCount(int count) {
        Debug.Log(count + " in setMirrorTouchCount");
        mirrorTouchCount = count;
    }

    public void incrementMirrorTouchCount() {
        mirrorTouchCount++;
        Debug.Log(mirrorTouchCount + " in incrementMirrorTouchCount");
    }


}