using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerModule : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public DamageData damageData;
    public GameObject projectilePrefab;


    public float RPM;
    public int RoundsPerMag;
    public int actualNumberOfRounds;
    public float reloadTime;
    public Camera weaponView;
    public LayerMask ls;

    public UnityEvent ReloadFeedback;

    [SerializeField] private IntScriptableEvent updateAmmo;
    [SerializeField] private FloatScriptableEvent updateReload;

    protected bool isReloading = false;

    /// <summary>
    /// Fall back when button is pressed
    /// </summary>
    public virtual void Hold()
    {
        if (!isReloading)
        {
        }
    }

    /// <summary>
    /// Fall back when button is released
    /// </summary>
    public virtual void Release()
    {
    }

    /// <summary>
    /// Do a raycast from the playerView to the direction the player is looking
    /// </summary>
    /// <param name="layerMask"></param>
    /// <returns>the Hit</returns>
    public virtual RaycastHit Raycast(LayerMask ls)
    {
        RaycastHit hit;
        Physics.Raycast(playerWeapon.inventory.playerController.playerView.transform.position, playerWeapon.inventory.playerController.playerView.transform.forward * 1000, out hit, 10000f, ls);
        return hit;
    }

    /// <summary>
    /// Shoot with the weapon
    /// </summary>
    public virtual void Shoot()
    {
        if (!isReloading)
        {
            updateAmmo.Trigger(actualNumberOfRounds);
            weaponView.GetComponent<CameraShake>().Shake();
        }
    }

    /// <summary>
    /// Call Reloaded() after reloadTime
    /// </summary>
    public virtual void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            Invoke("Reloaded", reloadTime);
            ReloadFeedback.Invoke();
            updateReload.Trigger(reloadTime);
        }
    }

    /// <summary>
    /// Reload the weapon
    /// </summary>
    public virtual void Reloaded()
    {
        isReloading = false;
        actualNumberOfRounds = RoundsPerMag;
        updateAmmo.Trigger(actualNumberOfRounds);
    }
}
