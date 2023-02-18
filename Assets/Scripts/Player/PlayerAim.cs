using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private bool isAiming;
    Ray cameraRay;
    RaycastHit cameraHit;
    Vector3 cameraXZHitPoint;
    Vector3 aimDirection;
    Quaternion lookAtQuaternion;
    [SerializeField] float aimSmoothness = 15f;

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButton(1)){
            cameraXZHitPoint= getXZHitPoint();
            if(cameraXZHitPoint.y != 0){
                isAiming= false;
                return;
            }
            isAiming= true;
            aimDirection= cameraXZHitPoint - new Vector3(transform.position.x, 0, transform.position.z);
            lookAtQuaternion= Quaternion.LookRotation(aimDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtQuaternion, Time.deltaTime * aimSmoothness);
        } else {
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
