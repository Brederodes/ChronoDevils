using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInfo : MonoBehaviour
{
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileLifeSpan;
    
    public bool isBeingShot;
    private float dissappearTime= -1f;

    // Update is called once per frame
    void Update()
    {
        if(isBeingShot){
            if(Time.time >= dissappearTime){
                isBeingShot= false;
                gameObject.SetActive(false);
            }
        }
    }

    public void setDisappearInTime(float dissappearTime){
        this.dissappearTime= Time.time + dissappearTime;
    }
 
    public void OnEnable(){
        gameObject.GetComponent<TrailRenderer>().Clear();
    }
    public void OnDisable(){
        gameObject.GetComponent<TrailRenderer>().Clear();
    }
}
