using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject projectileLaunchAnchor;

    public void HoldTrigger(InputValue val)
    {
        if (val.isPressed)
        {
            if (inventory.triggerModuleEquipped)
            {
                inventory.triggerModuleEquipped.Hold();
            }
        }
        else
        {
            if (inventory.triggerModuleEquipped)
            {
                inventory.triggerModuleEquipped.Release();
            }
        }
    }
}
