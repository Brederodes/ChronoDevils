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
        transform.position= target.transform.position + new Vector3(cameraXOffset, cameraHeight, cameraZOffset);

        targetDirection= target.transform.position - transform.position;

        transform.rotation= Quaternion.LookRotation(targetDirection, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= target.transform.position + new Vector3(cameraXOffset, cameraHeight, cameraZOffset);

        targetDirection= target.transform.position - transform.position;

        transform.rotation= Quaternion.LookRotation(targetDirection, Vector3.up);
    }
}
