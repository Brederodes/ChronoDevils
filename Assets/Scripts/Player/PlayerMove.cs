using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float moveSpeed= 1f;

    //inputs
    private float xInput= 0f;
    private float zInput= 0f;
    private float totalInputMagnitude= 0f;
    Vector3 totalInputVector;

    //rotation
    [SerializeField] float playerRotationSmoothness = 5f;
    private Quaternion lookAtQuaternion;

    //player states
    private bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody= GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        xInput= Input.GetAxis("Horizontal");
        zInput= Input.GetAxis("Vertical");

        //check if any input is being made, if not, do nothing
        if(!isInputing()){
            rigidBody.velocity = Vector3.zero;
            return;
        }
        totalInputMagnitude= Mathf.Sqrt(Mathf.Pow(xInput, 2) + Mathf.Pow(zInput, 2));

        //normalizing the input to make it fit in a circle, mainting the max magnitude of 1
        if(totalInputMagnitude > 1){
            xInput= xInput/totalInputMagnitude;
            zInput= zInput/totalInputMagnitude;
        }
        totalInputVector= new Vector3(xInput, 0, zInput);

        //OVERRIDES the current velocity to the "input" one
        rigidBody.velocity= totalInputVector * moveSpeed;


        //does not rotate if is aiming
        isAiming= GetComponent<PlayerAim>().getIsAiming();
        if(isAiming){
            return;
        }

        //making the character rotation
        lookAtQuaternion = Quaternion.LookRotation(totalInputVector, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtQuaternion, Time.deltaTime * playerRotationSmoothness);
    }
    public bool isInputing(){
        if((xInput < 0.1f && xInput > -0.1f) &&
        (zInput < 0.1f && zInput > -0.1f)){
            return false;
        } else {
            return true;
        }
    }
}
