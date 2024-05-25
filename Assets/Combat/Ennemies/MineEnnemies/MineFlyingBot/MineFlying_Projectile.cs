using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFlying_Projectile : MonoBehaviour
{
    public DamageData damageData;
    public float desintegrateTime;

    private void Start()
    {
        Invoke("Desintegrate", desintegrateTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ImpactZone>())
        {
            other.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes,damageData.damages,this.gameObject);
        }
        Destroy(gameObject);
    }   

    private void Desintegrate()
    {
        Destroy(gameObject);
    }
}
