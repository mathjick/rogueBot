using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModularProjectileBase : MonoBehaviour
{
    public Dictionary<string,GameObject> projectiles;
    public ModularProjectileBase nextEffect;
    public DamageData damageData;
    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "tag_ennemi":
                HitEnnemi(other);
                break;
            case "tag_player":
                HitPlayer();
                break;
            case "tag_solid":
                HitSolid();
                break;
            default:
                Debug.Log("Hit something non-tagged");
                break;
        }
    }

    public void HitEnnemi(Collider other)
    {
        other.GetComponent<LifeSystem>().TakeDamage(damageData.damagesTypes,damageData.damages,owner);
    }

    public void HitPlayer()
    {

    }

    public void HitSolid()
    {
        Destroy(gameObject);
    }
}
