using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerWeapon weapon;
    public BaseModule moduleEquiped;
    public List<BaseModule> modulesStocked;

    public void HoldTrigger(InputValue val)
    {
        weapon.HoldTrigger(val);
    }
}
