using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public PlayerController controller;
    [Space(1)]
    [Header("------------ Dash Parameters ------------")]
    [Space(1)]
    public float dashPower;
    public float cooldown;
    public float fricionModifier;
    public float timeBeforeResetFriction;

    private bool canActivate = true;

    private void Start()
    {
        timeBeforeResetFriction = timeBeforeResetFriction > cooldown ? cooldown : timeBeforeResetFriction;
    }
    public void Activate()
    {
        if (canActivate)
        {
            canActivate = false;
            controller.rb.AddForce(new Vector3(controller.rb.velocity.x * dashPower, 0, controller.rb.velocity.z * dashPower),ForceMode.Impulse);
            controller.playerMouvementSystem.modifyFriction(0.5f);
            Invoke("ResetFriction", 0.5f);
            Invoke("ReloadAbility", cooldown);
        }
    }

    public void ReloadAbility()
    {
        canActivate = true;
    }

    public void ResetFriction()
    {
        controller.playerMouvementSystem.modifyFriction();
    }
}
