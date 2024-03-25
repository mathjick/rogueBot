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
            if (inventory.moduleEquiped)
            {
                inventory.moduleEquiped.triggerOn();
            }
        }
        else
        {
            holdingTrigger = false;
            if (inventory.moduleEquiped)
            {
                inventory.moduleEquiped.triggerOff();
            }
        }
    }

    public void Shot()
    {
        RaycastHit hit;
        Physics.Raycast(inventory.playerController.playerView.transform.position, inventory.playerController.playerView.transform.forward * 1000,out hit);
        if (hit.collider && hit.collider.tag != "tag_player")
        {
            Debug.Log("yeah");
            var spawnSpec = projectileLaunchAnchor.transform;
            var projectile = Instantiate(projectilePrefab, spawnSpec.position,spawnSpec.rotation);
            projectile.GetComponent<Rigidbody>().AddForce((hit.point - projectileLaunchAnchor.transform.position).normalized * 200);
        }
    }
}
