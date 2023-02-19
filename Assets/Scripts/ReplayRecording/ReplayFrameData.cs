using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayFrameData
{
    public float frameTime {get; private set;}
    public Vector3 framePosition {get; private set;}
    public Quaternion frameRotation {get; private set;}
    public bool shotThisFrame {get; private set;}
    public bool isAiming {get; private set;}

    public ReplayFrameData(Vector3 framePosition,
    Quaternion frameRotation,
    float frameTime){
        this.framePosition = framePosition;
        this.frameRotation = frameRotation;
        this.frameTime = frameTime;
    }
}
