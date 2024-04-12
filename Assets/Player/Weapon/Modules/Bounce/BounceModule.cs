using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BounceModule : BaseModule
{
    public PhysicMaterial bounceMaterial;
    public int bounceCount;
    public UnityEvent FinalBounceCallBack;
    public UnityEvent NormalBounceCallBack;
    public UnityEvent ShootCallBack;

    public override void CallStart()
    {
        if (this.gameObject.GetComponentInParent<ModularProjectileBase>())
        {
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.material = bounceMaterial;
        }
        base.CallStart();
    }
    public override void CallOnHit(Collision other)
    {
        if (bounceCount <= 0)
        {
            FinalBounceCallBack?.Invoke();
            base.CallOnHit(other);
        }
        else
        {
            NormalBounceCallBack?.Invoke();
            base.HitEffect(other);
        }
        bounceCount--;
    }

    public override void CallOnShot()
    {
        ShootCallBack?.Invoke();
        base.CallOnShot();
    }
}
