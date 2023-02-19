using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance {get; private set;}
    public event Action<GameObject> onChangeCameraTarget;
    public event Action onPlayerRespawn;
    public event Action onFinishReach;

    private void Awake(){
        if(instance != null){
            Debug.Log("more than one game manager on the scene.");
        }
        instance= this;
    }

    public void changeCameraTarget(GameObject newTarget){
        if(onChangeCameraTarget != null){
            onChangeCameraTarget(newTarget);
        }
    }

    public void playerRespawn(){
        if(onPlayerRespawn != null){
            onPlayerRespawn();
        }
    }

    public void finishReach(){
        if(onFinishReach != null){
            onFinishReach();
        }
    }
}
