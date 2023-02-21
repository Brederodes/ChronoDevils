using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public bool hasDuration = false;
    [SerializeField] public int duration;
    [SerializeField] public int party;
    [SerializeField] public float hitDamage;
    private int originalDuration;
    private HealthMechanic healthComponentBeingHit;

    public void Awake(){
        if(hasDuration){
            originalDuration= duration;
        }
    }

    private void OnCollisionEnter(Collision collisionInfo){
        healthComponentBeingHit= collisionInfo.gameObject.GetComponent<HealthMechanic>();
        if(healthComponentBeingHit == null ){
            return;
        }
        if(healthComponentBeingHit.timeWasLastHit + healthComponentBeingHit.immunityTimeDuration > Time.time){
            return;
        }
        if(healthComponentBeingHit.party == party){
            return;
        }
        
        healthComponentBeingHit.health-= hitDamage;
        healthComponentBeingHit.timeWasLastHit= Time.time;
        if(healthComponentBeingHit.health <= 0f){
            healthComponentBeingHit.die();
        }
        if(hasDuration){
            duration -= 1;
            if(duration == 0){
                gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionStay(Collision collisionInfo){
        OnCollisionEnter(collisionInfo);
    }
    private void OnTriggerEnter(Collider collider){
        healthComponentBeingHit = collider.gameObject.GetComponent<HealthMechanic>();
        if(healthComponentBeingHit== null){
            return;
        }
        if(healthComponentBeingHit.timeWasLastHit + healthComponentBeingHit.immunityTimeDuration > Time.time){
            return;
        }
        if(healthComponentBeingHit.party == party){
            return;
        }
        healthComponentBeingHit.health-= hitDamage;
        healthComponentBeingHit.timeWasLastHit= Time.time;
        
        if(healthComponentBeingHit.health <= 0f){
            healthComponentBeingHit.die();
        }
        if(hasDuration){
            duration -= 1;
            if(duration == 0){
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider collider){
        OnTriggerEnter(collider);
    }
    public void OnEnable(){
        duration= originalDuration;
    }
}
