using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlasmaModule : BaseModule
{
    public float sizeFactor;
    public float slowDownFactor;
    public UnityEvent ShootCallBack;
    public UnityEvent StartCallBack;
    public UnityEvent CleanCallBack;

    public override void CallStart()
    {
        if (this.gameObject.GetComponentInParent<ModularProjectileBase>())
        {
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.velocity = this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.velocity * slowDownFactor;
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass = this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass * (1/slowDownFactor);
            this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale = this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale * sizeFactor;
        }
        StartCallBack?.Invoke();
        base.CallStart();
    }

    public override void CallOnShot()
    {
        ShootCallBack?.Invoke();
        base.CallOnShot();
    }

    public override void CallCleanItself()
    {
        CleanCallBack?.Invoke();
        base.CallCleanItself();
    }
}
