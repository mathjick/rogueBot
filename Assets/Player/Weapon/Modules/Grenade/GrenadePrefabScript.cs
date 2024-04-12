using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrenadePrefabScript : MonoBehaviour
{
    public DamageData damageData;
    public GameObject player;
    public float fuzeTime;
    public Collider explosionCollider;
    public UnityEvent ExplodeCallBack;
    public UnityEvent IgniteCallBack;

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    public void IgniteFuze()
    {
        IgniteCallBack.Invoke();
        Invoke("Explode", fuzeTime);
    }

    public void Explode()
    {
        ExplodeCallBack.Invoke();
        var result = Physics.OverlapSphere(transform.position, explosionCollider.bounds.size.x / 2);
        foreach (var hit in result)
        {
            if (hit.tag == "tag_ennemie")
            {
                hit.gameObject.GetComponent<ImpactZone>().TakeDamage(damageData.damagesTypes,damageData.damages,player);
            }
        }
        Destroy(gameObject);
    }
}
