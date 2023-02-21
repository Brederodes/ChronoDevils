using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    int round;
    float timeAtRoundStart;
    float roundTime;
    [SerializeField] float roundDuration;
    // Start is called before the first frame update
    void Start()
    {
        round= 0;
        timeAtRoundStart= 0;
        roundTime= 0;
    }

    // Update is called once per frame
    void Update()
    {
        roundTime= Time.time - timeAtRoundStart;
        GameObject.FindWithTag("UITimer").GetComponent<TextMeshProUGUI>().text = roundTime.ToString("F1");
        if(roundTime >= roundDuration){
            round++;
            GameEventManager.instance.endRound();
            timeAtRoundStart = Time.time;
            roundTime= 0;
        }
    }
}
