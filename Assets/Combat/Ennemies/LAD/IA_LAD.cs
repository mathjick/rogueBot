using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LADState
{
    Idle, Shoot, Discharge, Barrage, Call, Rain
}
public class IA_LAD : MonoBehaviour
{
    [Space(1)]
    [Header("---------------- Internal logic ----------------")]
    [Space(1)]
    public LADState currentState;
    public GameObject target;

    public GameObject turretCore;
    public GameObject turretBarrels;

    [Space(3)]
    [Header("---------------- Base Shot ----------------")]
    [Space(1)]

    public GameObject projectilePrefab;
    public DamageData projectileDamage;
    public List<GameObject> barrels;
    public float rpm = 60;
    private float rpmModifier = 1;
    private int currentBarrel = 0;
    private float timeBeforeNextShot;

    [Space(3)]
    [Header("---------------- Discharge ----------------")]
    [Space(1)]

    public DamageData dischargeDamage;
    public float dischargeCooldown;
    public float dischargeChargeTime;
    public MeshRenderer dischargeRenderer;
    private List<LifeSystem> ObjectsInDischarges = new List<LifeSystem>();


    public void SwitchState(LADState newState)
    {
        currentState = newState;
    }

    public void ActivateLAD()
    {
        currentState = LADState.Shoot;
        target = PlayerController.instance.gameObject;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case LADState.Idle:
                break;
            case LADState.Shoot:
                timeBeforeNextShot -= Time.deltaTime;
                TurnCore();
                TurnBarrels();
                if (timeBeforeNextShot <= 0)
                {
                    currentBarrel++;
                    if (currentBarrel >= barrels.Count)
                    {
                        currentBarrel = 0;
                    }
                    GameObject projectile = Instantiate(projectilePrefab, barrels[currentBarrel].transform.position, barrels[currentBarrel].transform.rotation);
                    projectile.GetComponent<DamageData>().damagesTypes = projectileDamage.damagesTypes;
                    projectile.GetComponent<DamageData>().damages = projectileDamage.damages;
                    projectile.GetComponent<LAD_MissileScript>().target = target;
                    projectile.GetComponent<Rigidbody>().AddForce(barrels[currentBarrel].transform.forward * 1000);
                    timeBeforeNextShot = 60f / (rpm * rpmModifier);
                }
                break;
            case LADState.Discharge:
                break;
            case LADState.Barrage:
                break;
            case LADState.Call:
                break;
            case LADState.Rain:
                break;
            default:
                break;
        }
    }

    private void TurnCore()
    {
        Vector3 targetDir = target.transform.position - turretCore.transform.position;
        float step = 0.5f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(turretCore.transform.forward, targetDir, step, 0.0f);
        newDir.y = 0;
        turretCore.transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void TurnBarrels()
    {
        Vector3 targetDir = target.transform.position - turretBarrels.transform.position;
        targetDir = targetDir.normalized;
        if( turretBarrels.transform.forward.y - targetDir.y > 0.1f)
        {
            turretBarrels.transform.Rotate(0.5f, 0,0);
        }
        else if (turretBarrels.transform.forward.y - targetDir.y < -0.1f)
        {
            turretBarrels.transform.Rotate(-0.5f, 0,0);
        }
    }
    
    private void ActivateDischarge()
    {
        this.SwitchState(LADState.Discharge);
        dischargeRenderer.enabled = true;
        Invoke("Discharge", dischargeChargeTime);
    }

    public void Discharge()
    {
        foreach (LifeSystem lifeSystem in ObjectsInDischarges)
        {
            lifeSystem.TakeDamage(dischargeDamage.damagesTypes, dischargeDamage.damages, gameObject);
        }
        dischargeRenderer.enabled = false;
    }

    public void EnterDischargeZone(Collider other)
    {
        if (other && other.GetComponent<LifeSystem>() && !ObjectsInDischarges.Contains(other.GetComponent<LifeSystem>()))
        {
            ObjectsInDischarges.Add(other.GetComponent<LifeSystem>());
        }
    }

    public void ExitDischargeZone(Collider other)
    {
        if (other.GetComponent<LifeSystem>() && ObjectsInDischarges.Contains(other.GetComponent<LifeSystem>()))
        {
            ObjectsInDischarges.Remove(other.GetComponent<LifeSystem>());
        }
    }
}
