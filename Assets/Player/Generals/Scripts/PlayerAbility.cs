using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAbility : MonoBehaviour
{
    public PlayerController controller;
    [Space(1)]
    [Header("------------ Dash Parameters ------------")]
    [Space(1)]
    public float dashPower;
    public float cooldown;
    public float frictionModifier = 1;
    public float timeBeforeResetFriction;

    private bool canActivate = true;

    [Space(1)]
    [Header("---------------- CallBack ----------------")]
    [Space(1)]

    public UnityEvent OnDashCallBack;

    private void Start()
    {
        timeBeforeResetFriction = timeBeforeResetFriction > cooldown ? cooldown : timeBeforeResetFriction;
    }
    public void Activate()
    {
        if (canActivate)
        {
            canActivate = false;
            OnDashCallBack?.Invoke();
            var input = controller.playerMouvementSystem.playerInput;
            var mouvement = (controller.transform.right * input.x) + (controller.transform.forward * input.y);
            controller.rb.AddForce(mouvement * dashPower, ForceMode.Impulse);
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
