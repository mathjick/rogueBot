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

    public void Start()
    {
        Invoke("CleanItself", 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "tag_ennemie":
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
        other.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes,damageData.damages,owner);
        CleanItself();
    }

    public void HitPlayer()
    {

    }

    public void HitSolid()
    {
        CleanItself();
    }

    public void CleanItself()
    {
        Destroy(gameObject);
    }
}
