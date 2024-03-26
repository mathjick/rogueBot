using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlasmaModule : BaseModule
{
    public float sizeFactor;
    public float slowDownFactor;

    public override void CallStart()
    {
        if (this.gameObject.GetComponentInParent<ModularProjectileBase>())
        {
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.velocity = this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.velocity * slowDownFactor;
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass = this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass * (1/slowDownFactor);
            this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale = this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale * sizeFactor;
        }
        base.CallStart();
    }
}
