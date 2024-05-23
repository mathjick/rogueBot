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

    public virtual RaycastHit Raycast(LayerMask ls)
    {
        RaycastHit hit;
        Physics.Raycast(playerWeapon.inventory.playerController.playerView.transform.position, playerWeapon.inventory.playerController.playerView.transform.forward * 1000, out hit, 10000f, ls);

        return hit;
    }

    public virtual void Shoot()
    {
        weaponView.gameObject.GetComponent<CameraShake>().Shake();
    }
}
