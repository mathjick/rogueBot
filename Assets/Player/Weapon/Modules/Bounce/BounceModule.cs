using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

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
        DispatchCollision(other);
        bounceCount--;
    }
    public void DispatchCollision(Collision other)
    {
        if (bounceCount <= 0)
        {
            base.CallOnHit(other);
        }
    }
}
