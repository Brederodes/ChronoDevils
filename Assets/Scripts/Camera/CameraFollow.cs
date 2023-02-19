using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float cameraHeight = 10f;
    [SerializeField] float cameraXOffset = 0;
    [SerializeField] float cameraZOffset = -10f;
    private Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject originalTarget = target;
        if(target != null){
            transform.position= target.transform.position + new Vector3(cameraXOffset, cameraHeight, cameraZOffset);

            targetDirection= target.transform.position - transform.position;

            transform.rotation= Quaternion.LookRotation(targetDirection, Vector3.up);
        }
        //subscribe to the events
        GameEventManager.instance.onChangeCameraTarget += onChangeCameraTarget;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null){
            transform.position= target.transform.position + new Vector3(cameraXOffset, cameraHeight, cameraZOffset);
            targetDirection= target.transform.position - transform.position;
            transform.rotation= Quaternion.LookRotation(targetDirection, Vector3.up);
        }
    }

    private void onChangeCameraTarget(GameObject newTarget){
        if(newTarget != null){
            target= newTarget;
        }
    }
}
