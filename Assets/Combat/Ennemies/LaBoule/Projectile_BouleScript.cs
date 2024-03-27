using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_BouleScript : MonoBehaviour
{
    public DamageData damageData;
    public float lifespan;
    public GameObject sourceOfDamage;

    private float liveTime;

    public void FixedUpdate()
    {
        liveTime += Time.deltaTime;
        if(liveTime > lifespan)
        {
            autoDelete();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "tag_solid")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.GetComponent<ImpactZone>())
        {
            collision.gameObject.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes,damageData.damages,sourceOfDamage);
            autoDelete();
        }
    }

    private void autoDelete()
    {
        Destroy(this.gameObject);
    }
}
