using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAD_MissileScript : MonoBehaviour
{
    public DamageData damageData;
    public float lifespan;
    public float magnetismForce = 10;
    public GameObject sourceOfDamage;
    public GameObject target;

    private float liveTime;

    public void FixedUpdate()
    {
        liveTime += Time.deltaTime;
        if (liveTime > lifespan)
        {
            autoDelete();
        }
        if(target != null)
        {
            GetComponent<Rigidbody>().AddForce(magnetismForce * (target.transform.position - transform.position).normalized);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "tag_solid")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.GetComponent<ImpactZone>())
        {
            collision.gameObject.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes, damageData.damages, sourceOfDamage);
            autoDelete();
        }
    }

    private void autoDelete()
    {
        Destroy(this.gameObject);
    }
}
