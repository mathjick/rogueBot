using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ModularProjectileBase : MonoBehaviour
{
    public Dictionary<string,GameObject> projectiles;
    public BaseModule nextEffect;
    public DamageData damageData;
    public GameObject owner;
    public Collider physicCollider;
    public Collider overlapCollider;

    public GameObject OnHitSolid;

    public void Start()
    {
        if (nextEffect)
        {
            nextEffect.CallStart();
        }
        Invoke("CleanItself", 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "tag_solid" && OnHitSolid)
        {
            Instantiate(OnHitSolid, transform.position, Quaternion.identity);
        }
        if (nextEffect)
        {
            nextEffect.CallOnTriggerEnter(other);
        }
        else
        {
            switch (other.tag)
            {
                case "tag_ennemie":
                    HitEnnemi(other);
                    break;
                case "tag_player":
                    HitPlayer();
                    break;
                case "tag_solid":
                    HitSolid();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (nextEffect)
        {
            nextEffect.CallOnHit(collision);
        }
    }

    public void HitEnnemi(Collider other)
    {
        overlapCollider.enabled = false;
        if (other.GetComponent<ImpactZone>())
        {
            Debug.Log("takeDamage");
            other.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes, damageData.damages, owner, other.ClosestPoint(transform.position));
        }
        CleanItself();
    }

    public void HitPlayer()
    {
        CleanItself();
    }

    public void HitSolid()
    {
        CleanItself();
    }

    public void CleanItself()
    {
        Destroy(gameObject);
    }
}
