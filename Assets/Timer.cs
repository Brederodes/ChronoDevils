using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    Transform spawnPosition;
    int round;
    float timeAtRoundStart;
    float roundTime;
    [SerializeField] float roundDuration;

    //QUICK TIMELINE TEST

    [SerializeField] GameObject replayObjectPrefab;
    List<ReplayObject> replayObjects = new List<ReplayObject>();
    List< Queue<ReplayFrameData> > replays = new List< Queue<ReplayFrameData> >();
    List< Queue<ReplayFrameData> > originalReplays = new List< Queue<ReplayFrameData> >();

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.onEndRound += onEndRound;
        round= 0;
        timeAtRoundStart= 0;
        roundTime= 0;

        spawnPosition= GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //set timer
        roundTime= Time.time - timeAtRoundStart;
        GameObject.FindWithTag("UITimer").GetComponent<TextMeshProUGUI>().text = roundTime.ToString("F1") + "| round "+ round;
        
        //deal with all timelines
        for(int i= 0; i < replayObjects.Count; i++){
            while(replays[i].Count != 0 && replays[i].Peek().frameTime <= roundTime ){
                replayObjects[i].setReplayFrame(replays[i].Dequeue());
            }
        }

        if(roundTime >= roundDuration){
            GameEventManager.instance.endRound();
        }
    }
    public void onEndRound(){
        round++;
        timeAtRoundStart = Time.time;
        roundTime= 0;

        replayObjects.Add(GameObject.Instantiate(replayObjectPrefab, spawnPosition.position, spawnPosition.rotation, transform).GetComponent<ReplayObject>());
        originalReplays.Add(GameObject.FindWithTag("Player").GetComponent<Recorder>().replayBeingRecorded);
        replays = new List< Queue<ReplayFrameData> >();
        foreach (Queue<ReplayFrameData> replay in originalReplays)
        {
            Queue<ReplayFrameData> newReplay = new Queue<ReplayFrameData>();
            foreach(ReplayFrameData rfd in replay){
                newReplay.Enqueue(rfd);
            }
            replays.Add(newReplay);
        }
        GameEventManager.instance.startRound();
    }
}
