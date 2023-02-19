using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    private bool isPlayerFinished= false;
    void Start(){
        GameEventManager.instance.onFinishReach += onFinishReach;
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            GameEventManager.instance.finishReach();
        }
    }
    void Update(){
        if(isPlayerFinished){
            if(Input.GetKeyDown("r")){
                Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
                playerCollider.enabled= true;
                MeshRenderer playerMeshRenderer = GameObject.FindWithTag("Player").GetComponent<MeshRenderer>();
                playerMeshRenderer.enabled= true;
                MonoBehaviour[] playerScripts= GameObject.FindWithTag("Player").GetComponents<MonoBehaviour>();
                foreach(MonoBehaviour playerScript in playerScripts){
                    playerScript.enabled= true;
                }
                isPlayerFinished= false;
                GameObject.FindWithTag("Player").transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                GameEventManager.instance.playerRespawn();
            }
        }
    }
    private void onFinishReach(){
        Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        playerCollider.enabled= false;
        Rigidbody playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        playerRigidbody.velocity= Vector3.zero;
        MeshRenderer playerMeshRenderer = GameObject.FindWithTag("Player").GetComponent<MeshRenderer>();
        playerMeshRenderer.enabled= false;
        MonoBehaviour[] playerScripts= GameObject.FindWithTag("Player").GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour playerScript in playerScripts){
            playerScript.enabled= false;
        }
        GameObject.FindWithTag("Player").GetComponent<Recorder>().enabled = true;
        isPlayerFinished= true;
    }
}
