using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    //float timeAtStartOfReplaying;
    float timeAtStartOfRecording;
    //[SerializeField] public GameObject replayPrefab;
    //ReplayObject replayObject;
    ReplayFrameData frameDataBeingRecorded;
    public Queue<ReplayFrameData> replayBeingRecorded {get; private set;}
    //public Queue<ReplayFrameData> replayBeingReplayed {get; private set;}
    //public bool isDoingReplay= false;

    void Awake(){
        replayBeingRecorded = new Queue<ReplayFrameData>();
        GameEventManager.instance.onStartRound += onStartRound;
    }

    void Start()
    {
        timeAtStartOfRecording= Time.time;
        //GameEventManager.instance.onFinishReach+= onFinishReach;
        //GameEventManager.instance.onPlayerRespawn+= onPlayerRespawn;
    }

    void LateUpdate(){
        //if(isDoingReplay) return;
        //RECORDING GAMEPLAY
        bool isAiming= GetComponent<PlayerAim>().isAiming;
        Vector3 actualAimDirection= GetComponent<PlayerAim>().actualAimDirection;
        bool shotThisFrame= GetComponentInChildren<PlayerShooter>().shotThisFrame;
        frameDataBeingRecorded = new ReplayFrameData(Time.time- timeAtStartOfRecording, transform.position, transform.rotation, isAiming, actualAimDirection, shotThisFrame);
        recordReplayFrameData(frameDataBeingRecorded);
    }
    void Update(){
        //REPLAYING GAMEPLAY
        /*
        if(isDoingReplay){
            if(playReplayFrame(replayObject)){
                return;
            }
            replayBeingReplayed= new Queue<ReplayFrameData>(replayBeingRecorded);
            startReplay();
        }
        */
    }

    /*private void startReplay(){
        timeAtStartOfReplaying= Time.time;
        replayBeingReplayed= new Queue<ReplayFrameData>(replayBeingRecorded);
        if(isDoingReplay){
            Destroy(replayObject.gameObject);
        }
        replayObject = GameObject.Instantiate(replayPrefab).GetComponent<ReplayObject>();
        replayObject.setReplayFrame(replayBeingReplayed.Peek());
        GameEventManager.instance.changeCameraTarget(replayObject.gameObject);
        isDoingReplay= true;
    }
    
    public void Reset(){
        timeAtStartOfRecording= Time.time;
        Destroy(replayObject.gameObject);
        replayBeingRecorded= new Queue<ReplayFrameData>();
    }*/
    
    public void recordReplayFrameData(ReplayFrameData frameData){
        replayBeingRecorded.Enqueue(frameData);
        //Debug.Log("Recorded data:" + frameData.framePosition + " " + frameData.frameRotation + " " + replayBeingRecorded.Count);
    }
    public void onStartRound(){
        replayBeingRecorded= new Queue<ReplayFrameData>();
        timeAtStartOfRecording = Time.time;
    }
    /*private bool playReplayFrame(ReplayObject replayObject){
        
        if(replayBeingReplayed.Count != 0){
            if(Time.time- timeAtStartOfReplaying < replayBeingReplayed.Peek().frameTime){
                return true;
            }
            replayObject.setReplayFrame(replayBeingReplayed.Dequeue());
            return true;
        }
        return false;
    }
    private void onPlayerRespawn(){
        GameEventManager.instance.changeCameraTarget(GameObject.FindWithTag("Player"));
        isDoingReplay= false;
        Reset();
    }
    private void onFinishReach(){
        startReplay();
    }*/
}
