using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerModule : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public DamageData damageData;

    public float RPM;

    public Camera weaponView;

    public virtual void Hold()
    {
    }

    public virtual void Release()
    {
    }

    public virtual void Shoot()
    {
        weaponView.gameObject.GetComponent<CameraShake>().Shake();
    }
}
