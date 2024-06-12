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
            Debug.Log("Weak Point " + this.gameObject.name);
            WeakPointCallBack.Invoke();
        }
        else
        {
            Debug.Log("Normal Hit " + this.gameObject.name);
            HitCallBack.Invoke();
        }
        //DamagePopup.Create(this.transform.position, trueDamageTaken, false);
        Debug.Log(this.transform.position);
    }

    public void TakeDamage(DamageTypes[] damageTypes, float damages, GameObject sourceOfDamage, Vector3 popUpPos)
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
            Debug.Log("Weak Point " + this.gameObject.name);
            WeakPointCallBack.Invoke();
        }
        else
        {
            Debug.Log("Normal Hit " + this.gameObject.name);
            HitCallBack.Invoke();
        }
        DamagePopUpGenerator.instance.CreatePopUp(popUpPos, trueDamageTaken.ToString(), isWeakPoint);
        //DamagePopup.Create(this.transform.position, trueDamageTaken, false);
        Debug.Log(this.transform.position);
    }
}
