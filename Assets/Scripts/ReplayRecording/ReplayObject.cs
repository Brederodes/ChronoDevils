using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayObject : MonoBehaviour
{
    public void setReplayFrame(ReplayFrameData replayFrameData){
        transform.position = replayFrameData.framePosition;
        transform.rotation = replayFrameData.frameRotation;
    }
}
