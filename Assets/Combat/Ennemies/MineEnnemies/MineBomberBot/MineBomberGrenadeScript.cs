using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBomberGrenadeScript : MonoBehaviour
{
    public float fuzeTime;
    public DamageData damageData;
    public TriggerRelay triggerRelay;
    private List<LifeSystem> entityInExplosionRange = new List<LifeSystem>();
   

    public void LitFuze()
    {
        Invoke("Explode", fuzeTime);
    }

    public void Explode()
    {
        foreach (LifeSystem entity in entityInExplosionRange)
        {
            entity.TakeDamage(damageData.damagesTypes,damageData.damages,this.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void actualizeEntityInRange()
    {
        entityInExplosionRange.Clear();
        foreach (Collider col in triggerRelay.collidersIn)
        {
            LifeSystem ls = col.GetComponent<LifeSystem>();
            if (ls)
            {
                entityInExplosionRange.Add(ls);
            }
        }
    }

}
