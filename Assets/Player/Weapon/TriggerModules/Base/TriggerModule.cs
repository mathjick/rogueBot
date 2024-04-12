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
        Debug.Log("Hold with module");
    }

    public virtual void Release()
    {
        Debug.Log("Release with module");
    }

    public virtual void Shoot()
    {
        Debug.Log("Shoot!");
        weaponView.gameObject.GetComponent<CameraShake>().Shake();
    }
}
