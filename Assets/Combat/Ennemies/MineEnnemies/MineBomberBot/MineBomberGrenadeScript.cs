using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBomberGrenadeScript : MonoBehaviour
{
    public float fuzeTime;
    public float explosionRange;
    private List<LifeSystem> entityInExplosionRange = new List<LifeSystem>();
   

    public void LitFuze()
    {
        Invoke("Explode", fuzeTime);
    }

    public void Explode()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LifeSystem>() && !entityInExplosionRange.Contains(other.GetComponent<LifeSystem>()))
        {
            entityInExplosionRange.Add(other.GetComponent<LifeSystem>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LifeSystem>() && entityInExplosionRange.Contains(other.GetComponent<LifeSystem>()))
        {
            entityInExplosionRange.Remove(other.GetComponent<LifeSystem>());
        }
    }

    private void OnDrawGizmos()
    {
    }
}
