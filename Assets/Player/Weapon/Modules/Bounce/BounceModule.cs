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
        Debug.Log("Je bounce");
        bounceCount--;
        base.CallOnHit(other);
    }

    public override void CallOnTriggerEnter(Collider other)
    {
        if(bounceCount <= 0)
        {
            switch (other.tag)
            {
                case "tag_ennemie":
                    other.GetComponent<ImpactZone>().TakeDamage(this.gameObject.GetComponentInParent<ModularProjectileBase>().damageData.damagesTypes, this.gameObject.GetComponentInParent<ModularProjectileBase>().damageData.damages, this.gameObject.GetComponentInParent<ModularProjectileBase>().owner);
                    break;
                case "tag_player":
                    base.CallCleanItself();
                    break;
                case "tag_solid":
                    base.CallCleanItself();
                    break;
                default:
                    Debug.Log("Hit something non-tagged");
                    break;
            }
        }
    }
}
