using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAD_Bomb : MonoBehaviour
{
    public float fuzeTime = 5;
    public DamageData damageData;
    private float fuzeTimer;
    private List<LifeSystem> entitysIn = new List<LifeSystem>();
    public TriggerRelay relayExplosionZone;
    // Start is called before the first frame update
    void Start()
    {
        fuzeTimer = fuzeTime;
    }

    public void Update()
    {
        fuzeTimer -= Time.deltaTime;
        if (fuzeTimer <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        foreach (LifeSystem entity in entitysIn)
        {
            entity.TakeDamage(damageData.damagesTypes, damageData.damages,gameObject);
        }
        Destroy(gameObject);
    }

    public void actualiseEntityInExplosionZone()
    {
        entitysIn = new List<LifeSystem>();
        foreach (Collider entity in relayExplosionZone.collidersIn)
        {
            LifeSystem entityLifeSystem = entity.GetComponent<LifeSystem>();
            if (entityLifeSystem != null && !entitysIn.Contains(entityLifeSystem))
            {
                entitysIn.Add(entityLifeSystem);
            }
        }
    }
}
