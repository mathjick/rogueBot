using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularProjectileBase : MonoBehaviour
{
    public Dictionary<string,GameObject> projectiles;
    public ModularProjectileBase nextEffect;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void HitEnnemi()
    {

    }

    public void HitPlayer()
    {

    }

    public void HitSolid()
    {

    }
}
