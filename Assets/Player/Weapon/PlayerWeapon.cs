using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject projectileLaunchAnchor;
    public GameObject projectilePrefab;

    public float delayBetweenShots = 0;
    public float delayRemaining = 0;

    public DamageTypes[] damageTypes;
    private bool holdingTrigger = false;

    public void FixedUpdate()
    {
        if (delayRemaining > 0)
        {
            delayRemaining -= Time.deltaTime;
        }
        else if (holdingTrigger)
        {
            delayRemaining = delayBetweenShots;
            Shot();
        }
    }

    public void HoldTrigger(InputValue val)
    {
        if (val.isPressed)
        {
            holdingTrigger = true;
        }
        else
        {
            holdingTrigger = false;
        }
    }

    public void Shot()
    {
        RaycastHit hit;
        Physics.Raycast(inventory.playerController.playerView.transform.position, inventory.playerController.playerView.transform.forward * 1000,out hit);
        if (hit.collider)
        {
            Debug.DrawLine(inventory.playerController.playerView.transform.position, hit.point, Color.red, 10);
        }
        if (hit.collider && hit.collider.gameObject.GetComponent<LifeSystem>())
        {
            //hit.collider.gameObject.GetComponent<LifeSystem>().TakeDamage(damageTypes, 0,inventory.playerController.gameObject);
        }
    }
}
