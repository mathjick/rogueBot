using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModule : MonoBehaviour
{
    public BaseModule nextModule;

    public virtual void CallStart()
    {
        if (nextModule) {
            nextModule.CallStart();
        }
    }

    public virtual void CallOnTriggerEnter(Collider other)
    {
        if(nextModule)
        {
            nextModule.CallOnTriggerEnter(other);
        }
    }

    public virtual void CallOnHit(Collision other)
    {
        if (nextModule)
        {
            nextModule.CallOnHit(other);
        }
        else
        {
            switch (other.collider.tag)
            {
                case "tag_ennemie":
                    other.collider.GetComponent<ImpactZone>().TakeDamage(this.gameObject.GetComponentInParent<ModularProjectileBase>().damageData.damagesTypes, this.gameObject.GetComponentInParent<ModularProjectileBase>().damageData.damages, this.gameObject.GetComponentInParent<ModularProjectileBase>().owner);
                    break;
                case "tag_player":
                    Debug.Log("Hit the player");
                    break;
                case "tag_solid":
                    this.CallCleanItself();
                    break;
                default:
                    Debug.Log("Hit something non-tagged");
                    break;
            }
        }
    }

    public virtual void HitEffect(Collision other)
    {
        if (nextModule)
        {
            nextModule.HitEffect(other);
        }
    }

    public virtual void CallOnShot()
    {
        if (nextModule)
        {
            nextModule.CallOnShot();
        }
    }

    public virtual void CallCleanItself()
    {
        if (this.gameObject.GetComponentInParent<ModularProjectileBase>())
        {
            this.gameObject.GetComponentInParent<ModularProjectileBase>().CleanItself();
        }
        else
        {
            Debug.Log("Not attached to a projectile");
        }
    }

    public virtual BaseModule attachToProjectile(ModularProjectileBase projectile)
    {
        var newModule = Instantiate(this, projectile.gameObject.transform);
        if (nextModule)
        {
            var buffer = nextModule.attachToProjectile(projectile);
            newModule.nextModule = buffer;
        }
        return newModule;
    }
}
