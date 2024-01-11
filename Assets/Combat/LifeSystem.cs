using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum DamageTypes
{
    cineticDamage,ionizedDamage,explosiveDamage
}

[System.Serializable]
public struct DamageTypesArmor
{
    public DamageTypes damageType;
    public float percentReduction;
    public int flatReduction;
}

public class LifeSystem : MonoBehaviour
{
    public bool immune;
    public int lifePoints;
    public int maxLifePoints;
    public DamageTypesArmor[] damageTypesArmors;
    private GameObject lastSourceOfDamage;

    [SerializeField] private UnityEvent OnDeath;

    public void Start()
    {
        lifePoints = maxLifePoints;
    }
    public void TakeDamage(DamageTypes[] damageTypes, float damages, GameObject sourceOfDamage)
    {
        lastSourceOfDamage = sourceOfDamage;
        var trueDamageTaken = damages;
        foreach (var armor in this.damageTypesArmors)
        {
            if (damageTypes.Contains<DamageTypes>(armor.damageType))
            {
                trueDamageTaken -= trueDamageTaken*armor.percentReduction;
                trueDamageTaken -= armor.flatReduction;
            }
        }
        trueDamageTaken = Mathf.CeilToInt(trueDamageTaken);
        lifePoints -= (int)trueDamageTaken;
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        OnDeath.Invoke();
    }
}
