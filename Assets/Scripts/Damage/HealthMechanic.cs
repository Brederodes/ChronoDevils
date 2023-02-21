using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMechanic : MonoBehaviour
{
    [SerializeField] public float maxHealh;
    [SerializeField] public float health;
    [SerializeField] public Collider hitbox;
    [SerializeField] public float immunityTimeDuration;
    [SerializeField] public int party;
    public float timeWasLastHit;

    public void Start(){
        timeWasLastHit= -immunityTimeDuration;
        health= maxHealh;
    }

    public void die(){
        gameObject.SetActive(false);
    }
}
