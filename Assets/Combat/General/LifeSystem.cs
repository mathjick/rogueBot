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
    [SerializeField] private UnityEvent OnDamage;

    public void Start()
    {
        lifePoints = maxLifePoints;
        if (PvNBr)
        {
            PvNBr.text = lifePoints.ToString();
        }
    }
    public void TakeDamage(DamageTypes[] damageTypes, float damages, GameObject sourceOfDamage)
    {
        if (lifePoints > 0)
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
            if (trueDamageTaken > 0)
            {
                OnDamage.Invoke();
            }
            lifePoints -= (int)trueDamageTaken;
            if (PvNBr)
            {
                PvNBr.text = lifePoints.ToString();
            }
            CheckForDeath();
        }
       
    }

    public void CheckForDeath()
    {
        if(lifePoints <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public void Diseapear(float timeToDiseapear)
    {
        Invoke("DestroyGameobject", timeToDiseapear);
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
