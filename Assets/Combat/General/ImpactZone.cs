using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ImpactZone : MonoBehaviour
{
    public LifeSystem lifeSystemAttached;
    public DamageTypesArmor[] damageTypesArmors;
    public bool isWeakPoint;

    public UnityEvent HitCallBack;
    public UnityEvent WeakPointCallBack;

    public void TakeDamage(DamageTypes[] damageTypes, float damages, GameObject sourceOfDamage)
    {
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
        lifeSystemAttached.TakeDamage(damageTypes, trueDamageTaken, sourceOfDamage);
        if (isWeakPoint)
        {
            WeakPointCallBack.Invoke();
        }
        else
        {
            HitCallBack.Invoke();
        }
    }
}
