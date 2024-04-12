using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrenadeModule : BaseModule
{
    public GameObject explosionPrefab;
    public float sizeFactor;
    public float gravityFactor;
    public GameObject player;
    public DamageData damageData;
    public UnityEvent ShootCallBack;
    public override void CallStart()
    {
        if (this.gameObject.GetComponentInParent<ModularProjectileBase>())
        {
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.useGravity = true;
            this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass = this.gameObject.GetComponentInParent<ModularProjectileBase>().physicCollider.attachedRigidbody.mass * gravityFactor;
            this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale = this.gameObject.GetComponentInParent<ModularProjectileBase>().transform.localScale * sizeFactor;
        }
        base.CallStart();
    }

    public override void CallOnHit(Collision other)
    {
        this.HitEffect(other);
        base.CallOnHit(other);
    }

    public override void HitEffect(Collision other)
    {
        if (explosionPrefab)
        {
            GameObject explosion = Instantiate(explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
            explosion.transform.localScale = this.gameObject.transform.localScale;
            explosion.GetComponent<GrenadePrefabScript>().player = player;
            explosion.GetComponent<GrenadePrefabScript>().damageData = this.gameObject.GetComponentInParent<ModularProjectileBase>().damageData;
            explosion.GetComponent<GrenadePrefabScript>().IgniteFuze();
        }
        base.HitEffect(other);
    }

    public override BaseModule attachToProjectile(ModularProjectileBase projectile)
    {
        var newModule = Instantiate(this, projectile.gameObject.transform);
        newModule.player = this.GetComponentInParent<PlayerInventory>().gameObject;
        newModule.damageData = this.damageData;
        if (nextModule)
        {
            var buffer = nextModule.attachToProjectile(projectile);
            newModule.nextModule = buffer;
        }
        return newModule;
    }

    public override void CallOnShot()
    {
        ShootCallBack?.Invoke();
        base.CallOnShot();
    }
}
