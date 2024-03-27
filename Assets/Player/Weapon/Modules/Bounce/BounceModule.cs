using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceModule : BaseModule
{
    public PhysicMaterial bounceMaterial;
    public int bounceCount;

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
            base.CallOnHit(other);
        }
        else
        {
            base.HitEffect(other);
        }
        bounceCount--;
    }
}
