using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModule : MonoBehaviour
{
    public BaseModule nextModule;

    public virtual void CallStart()
    {
    }

    public virtual void CallOnTriggerEnter(Collider other)
    {
    }

    public virtual void CallOnHit(Collision other)
    {
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
