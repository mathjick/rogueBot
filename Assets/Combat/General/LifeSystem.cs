using System.Linq;
using TMPro;
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
    public TextMeshProUGUI PvNBr;
    public GameObject lastSourceOfDamage;

    [SerializeField] private UnityEvent OnDeath;

    public void Start()
    {
        lifePoints = maxLifePoints;
        PvNBr.text = lifePoints.ToString();
    }
    public void TakeDamage(DamageTypes[] damageTypes, float damages, GameObject sourceOfDamage)
    {
        lastSourceOfDamage = sourceOfDamage;
        var trueDamageTaken = damages;
        if (damageTypesArmors.Length > 0)
        {
            foreach (var armor in this.damageTypesArmors)
            {
                if (damageTypes.Contains<DamageTypes>(armor.damageType))
                {
                    trueDamageTaken -= trueDamageTaken * armor.percentReduction;
                    trueDamageTaken -= armor.flatReduction;
                }
            }
        }
        trueDamageTaken = Mathf.CeilToInt(trueDamageTaken);
        lifePoints -= (int)trueDamageTaken;
        PvNBr.text = lifePoints.ToString();
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        if(lifePoints <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
