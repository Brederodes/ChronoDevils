using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] public bool isAiming {get; private set;}
    [SerializeField] public float aimLineLength= 5f;
    public Vector3 actualAimDirection {get; private set;}
    Ray cameraRay;
    RaycastHit cameraHit;
    LineRenderer aimRay;
    Vector3 cameraXZHitPoint;
    Vector3 desiredAimDirection;
    Quaternion lookAtQuaternion;
    [SerializeField] float aimSmoothness = 15f;

    void Start(){
        aimRay= gameObject.GetComponent<LineRenderer>();
    }

    void LateUpdate(){
        if(Input.GetMouseButton(1)){
            cameraXZHitPoint= getXZHitPoint();
            if(cameraXZHitPoint.y != 0){
                isAiming= false;
                return;
            }
            isAiming= true;

            desiredAimDirection= cameraXZHitPoint - new Vector3(transform.position.x, 0, transform.position.z);
            lookAtQuaternion= Quaternion.LookRotation(desiredAimDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtQuaternion, Time.deltaTime * aimSmoothness);
            
            actualAimDirection= transform.forward;

            aimRay.enabled= true;
            aimRay.SetPosition(0, transform.position);
            aimRay.SetPosition(1, transform.position+transform.forward*aimLineLength);
            
        } else {
            aimRay.enabled= false;
            isAiming= false;
        }
    }
    private Vector3 getXZHitPoint (){
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraHit, Mathf.Infinity, LayerMask.GetMask("AimPlane"), QueryTriggerInteraction.Collide))
        {
            return new Vector3(cameraHit.point.x, 0, cameraHit.point.z);
        }
        return Vector3.down;
    }
    public bool getIsAiming(){
        return isAiming;
    }
}
