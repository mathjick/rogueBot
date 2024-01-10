using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public PlayerController controller;
    public float dashPower;
    public float cooldown;

    private bool canActivate = true;
    public void Activate()
    {
        if (canActivate)
        {
            canActivate = false;
            controller.rb.AddForce(new Vector3(controller.rb.velocity.x * dashPower, 0, controller.rb.velocity.z * dashPower));
            Invoke("ReloadAbility", cooldown);
        }
    }

    public void ReloadAbility()
    {
        canActivate = true;
    }
}
