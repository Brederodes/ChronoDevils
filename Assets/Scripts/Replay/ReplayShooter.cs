using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayShooter : MonoBehaviour
{
    [SerializeField] ObjectPool projectilePool;

    void Start(){
        projectilePool = GameObject.FindWithTag("EnergyShotPool").GetComponent<ObjectPool>();
    }
    public void shoot(Vector3 aimDirection){
        GameObject projectile = projectilePool.GetPooledObject();
        if(projectile==null) return;
        projectile.transform.position= transform.position;
        projectile.transform.rotation= transform.rotation;

        float projectileSpeed = projectile.GetComponent<ProjectileInfo>().projectileSpeed;
        float projectileLifeSpan = projectile.GetComponent<ProjectileInfo>().projectileLifeSpan;

        projectile.GetComponent<Rigidbody>().velocity= aimDirection * projectileSpeed;
        projectile.GetComponent<ProjectileInfo>().setDisappearInTime(projectileLifeSpan);
        projectile.GetComponent<ProjectileInfo>().isBeingShot= true;

        projectile.SetActive(true);
    }
}
