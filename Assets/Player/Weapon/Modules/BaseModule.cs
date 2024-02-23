using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModule : MonoBehaviour
{
    public BaseModule nextModule;

    public virtual void triggerOn()
    {
        Debug.Log("Base TriggerOn");
        if (nextModule)
        {
            nextModule.triggerOn();
        }
    }

    public virtual void triggerOff()
    {
        Debug.Log("Base TriggerOff");
        if (nextModule)
        {
            nextModule.triggerOff();
        }
    }

    public virtual void generateProjectile(Vector3 position, Vector3 direction)
    {
        Debug.Log("Base GenerateProjectile");
        if (nextModule)
        {
            nextModule.generateProjectile(position, direction);
        }
    }

    public virtual void OnHit()
    {
        Debug.Log("Base OnHit");
        if (nextModule)
        {
            nextModule.OnHit();
        }
    }
}
