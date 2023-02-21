using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] PlayerAim playerAim;
    [SerializeField] ObjectPool projectilePool;
    [SerializeField] float shootingCooldownDuration;
    public bool shotThisFrame= false;
    private float nextPossibleShootingTime= -1f;


    void Update()
    {
        if(playerAim.isAiming){
            if(Input.GetMouseButtonDown(0)){
                
                if(Time.time < nextPossibleShootingTime){
                    return;
                }
                shotThisFrame= true;
                shoot();
                return;
            } else {
                shotThisFrame= false;
                return;
            }
        } else {
            shotThisFrame= false;
        }
        
    }

    void shoot(){
        nextPossibleShootingTime = Time.time + shootingCooldownDuration;
        GameObject projectile = projectilePool.GetPooledObject();
        if(projectile== null) return;
        projectile.transform.position= transform.position;
        projectile.transform.rotation= transform.rotation;

        float projectileSpeed = projectile.GetComponent<ProjectileInfo>().projectileSpeed;
        float projectileLifeSpan = projectile.GetComponent<ProjectileInfo>().projectileLifeSpan;

        projectile.GetComponent<Rigidbody>().velocity= playerAim.actualAimDirection * projectileSpeed;
        projectile.GetComponent<ProjectileInfo>().setDisappearInTime(projectileLifeSpan);
        projectile.GetComponent<ProjectileInfo>().isBeingShot= true;

        projectile.SetActive(true);
    }
}
