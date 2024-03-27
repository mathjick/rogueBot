using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Boule : IaBase
{
    public bool isAlive = true;
    public LifeSystem lifeSystem;
    public GameObject projectile;
    public GameObject projectileSummonPoint;

    public float distanceToShot;
    public float distanceToApproche;

    private bool canShot = true;

    private void FixedUpdate()
    {
        if(isAlive)
        {
            if (lifeSystem.lastSourceOfDamage != null)
            {
                if (Vector3.Distance(transform.position, lifeSystem.lastSourceOfDamage.transform.position) < distanceToShot)
                {
                    if (canShot)
                    {
                        ShotPlayer();
                        canShot = false;
                        Invoke("ResetCanShot", 2f);
                    }
                }
                if(Vector3.Distance(transform.position, lifeSystem.lastSourceOfDamage.transform.position) > distanceToApproche)
                {
                    transform.position = Vector3.MoveTowards(transform.position, lifeSystem.lastSourceOfDamage.transform.position, 0.2f);
                    transform.LookAt(lifeSystem.lastSourceOfDamage.transform);
                }
            }
        }
    }

    private void ShotPlayer()
    {
        //instansiate projectile and launch at player
        GameObject newProjectile = Instantiate(projectile, projectileSummonPoint.transform.position, projectileSummonPoint.transform.rotation);
        Vector3 direction = ((lifeSystem.lastSourceOfDamage.transform.position + (lifeSystem.lastSourceOfDamage.GetComponent<Rigidbody>().velocity * 0.5f ) ) - transform.position - new Vector3(0, 2, 0)).normalized * 2000;
        newProjectile.GetComponent<Rigidbody>().AddForce(direction);
        newProjectile.GetComponent<Projectile_BouleScript>().sourceOfDamage = gameObject;
    }

    public void ResetCanShot()
    {
        canShot = true;
    }

    override public void Dead()
    {
        isAlive = false;
        base.Dead();
    }
}
