using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpikyEnemyBehaviour : MonoBehaviour
{
    [SerializeField] public NavMeshAgent enemyNavMeshAgent;
    [SerializeField] public GameObject target;

    private Vector3 enemyCoord;
    private Vector3 targetCoord;
    private Vector3 distanceFromTargetVector;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Acquire all necessary cords and distances
        if(!acquireTargetCoords()){
            return;
        }
        followTarget();
    }

    private bool acquireTargetCoords(){
        if(Physics.Raycast(target.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)){
            targetCoord = hitInfo.point;
        } else {
            return false;
        }
        return true;
    }
    private void followTarget(){
        enemyNavMeshAgent.SetDestination(targetCoord);
    }
}
