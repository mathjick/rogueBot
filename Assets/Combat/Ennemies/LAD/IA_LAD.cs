using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum LADState
{
    Idle, Shoot, Discharge, Barrage, Call, BombRain, Dead
}

public enum LADWeakPoint
{
    None, Core, Back
}

[System.Serializable]
public struct WeakPointPair
{
    public BoxCollider collider;
    public LADWeakPoint weakPoint;
    public Light light;
}
public class IA_LAD : MonoBehaviour
{
    [Space(1)]
    [Header("---------------- Internal logic ----------------")]
    [Space(1)]
    public LADState currentState;
    public LADWeakPoint revealedWeakPoint;
    public GameObject target;
    public GameObject turretCore;
    public GameObject turretBarrels;
    public int currentBombSpawnPoint = 0;
    public List<WeakPointPair> pairs;

    [Space(3)]
    [Header("---------------- General ----------------")]
    [Space(1)]

    public float calmTime = 5;
    [Range(0, 1)]
    public float damageZoneHpTreshold = 0.5f;
    public Animation damageThresholdAnimation;
    private float calmTimer;

    [Space(3)]
    [Header("---------------- Base Shot ----------------")]
    [Space(1)]

    public GameObject projectilePrefab;
    public DamageData projectileDamage;
    public List<GameObject> barrels;
    public float rpm = 60;
    public float spread = 1;
    public float barrelRotationSpeed = 0.5f;
    public float coreRotationSpeed = 0.5f;
    private float rpmModifier = 1;
    private int currentBarrel = 0;
    private float timeBeforeNextShot;

    [Space(3)]
    [Header("---------------- Discharge ----------------")]
    [Space(1)]

    public DamageData dischargeDamage;
    public float dischargeCooldown = 10;
    public float dischargeChargeTime = 3;
    public MeshRenderer dischargeRenderer;
    private List<LifeSystem> ObjectsInDischarges = new List<LifeSystem>();
    private float dischargeTimer;

    [Space(3)]
    [Header("---------------- Barrage ----------------")]
    [Space(1)]

    public DamageData barrageDamage;
    public float barrageRpmMultiplyer = 2;
    public float barrageCooldown = 10;
    public float barrageTime = 5;
    public float barrageBarrelRotationSpeed = 0.5f;
    public float barrageCoreRotationSpeed = 0.5f;
    private float barrageTimer;

    [Space(3)]
    [Header("---------------- BombRain ----------------")]
    [Space(1)]

    public DamageData bombDamage;
    public GameObject bombPrefab;
    public float bombCooldown;
    public float timeBetweenBombs;
    public List<GameObject> bombSpawnPoints;
    private float bombTimer;

    [Space(3)]
    [Header("---------------- EventCallBacks ----------------")]
    [Space(1)]

    public UnityEvent BaseShotCallBack;
    public UnityEvent BombRainCallBack;
    public UnityEvent StartBarrageCallBack;
    public UnityEvent EndBarrageCallBack;
    public UnityEvent DischargeCallBack;
    public UnityEvent SwitchWeakPointCallBack;

    private void Start()
    {
        calmTimer = calmTime;
        timeBeforeNextShot = 60f / rpm;
        dischargeTimer = dischargeCooldown;
        barrageTimer = barrageCooldown;
        bombTimer = bombCooldown;
    }
    public void SwitchState(LADState newState)
    {
        if(currentState != LADState.Dead)
        {
            currentState = newState;
            if (newState == LADState.Shoot)
            {
                RevealWeakPoint(LADWeakPoint.None);
            }
            switch (newState)
            {
                case LADState.Idle:
                    break;
                case LADState.Shoot:
                    break;
                case LADState.Discharge:
                    break;
                case LADState.Barrage:
                    break;
                case LADState.Call:
                    break;
                case LADState.BombRain:
                    break;
                default:
                    break;
            }
        }
    }

    public void ActivateLAD()
    {
        currentState = LADState.Shoot;
        target = PlayerController.instance.gameObject;
    }

    private void FixedUpdate()
    {
        if (GetComponent<LifeSystem>().lifePoints < (GetComponent<LifeSystem>().maxLifePoints * damageZoneHpTreshold))
        {
            damageThresholdAnimation.Play();
        }
        switch (currentState)
        {
            case LADState.Idle:
                break;
            case LADState.Shoot:
                timeBeforeNextShot -= Time.deltaTime;
                calmTimer -= Time.deltaTime;
                shootLogic();
                break;
            case LADState.Discharge:
                break;
            case LADState.Barrage:
                timeBeforeNextShot -= Time.deltaTime;
                BarrageLogic();
                break;
            case LADState.Call:
                break;
            case LADState.BombRain:
                break;
            default:
                break;
        }
        barrageTimer -= Time.deltaTime;
        dischargeTimer -= Time.deltaTime;
        bombTimer -= Time.deltaTime;
    }

    private void RevealWeakPoint(LADWeakPoint weakPointToReveal)
    {
        foreach (WeakPointPair pair in pairs)
        {
            if (pair.weakPoint == weakPointToReveal)
            {
                pair.collider.enabled = true;
                pair.light.enabled = true;
            }
            else
            {
                pair.collider.enabled = false;
                pair.light.enabled = false;
            }
        }
        SwitchWeakPointCallBack?.Invoke();
    }

    private void TurnCore(float rotationSpeed)
    {
        Vector3 targetDir = target.transform.position - turretCore.transform.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(turretCore.transform.forward, targetDir, step, 0.0f);
        newDir.y = 0;
        turretCore.transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void TurnBarrels(float rotationSpeed)
    {
        Vector3 targetDir = target.transform.position - turretBarrels.transform.position;
        targetDir = targetDir.normalized;
        if (turretBarrels.transform.forward.y - targetDir.y > 0.1f)
        {
            turretBarrels.transform.Rotate(rotationSpeed, 0, 0);
        }
        else if (turretBarrels.transform.forward.y - targetDir.y < -0.1f)
        {
            turretBarrels.transform.Rotate(-rotationSpeed, 0, 0);
        }
    }

    public void Dead()
    {
        SwitchState(LADState.Dead);
    }

    #region Shoot

    private void shootLogic()
    {
        TurnCore(coreRotationSpeed);
        TurnBarrels(barrelRotationSpeed);
        if (timeBeforeNextShot <= 0)
        {
            this.shootBase();
            timeBeforeNextShot = 60f / (rpm * rpmModifier);
        }
        checkForSpecial();
    }
    private void shootBase()
    {
        currentBarrel++;
        if (currentBarrel >= barrels.Count)
        {
            currentBarrel = 0;
        }
        BaseShotCallBack?.Invoke();
        GameObject projectile = Instantiate(projectilePrefab, barrels[currentBarrel].transform.position, barrels[currentBarrel].transform.rotation);
        projectile.GetComponent<DamageData>().damagesTypes = projectileDamage.damagesTypes;
        projectile.GetComponent<DamageData>().damages = projectileDamage.damages;
        projectile.GetComponent<LAD_MissileScript>().target = target;
        projectile.GetComponent<Rigidbody>().AddForce(barrels[currentBarrel].transform.right * -1 * 1000);
    }

    private void checkForSpecial()
    {
        if(calmTimer <= 0)
        {
            // check if LAD is seing the player
            if ( Vector3.Dot(turretCore.transform.forward, target.transform.position - turretCore.transform.position) > 0.5f)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < 5f)
                {
                    if (dischargeTimer <= 0)
                    {
                        RevealWeakPoint(LADWeakPoint.Back);
                        Invoke("ActivateDischarge", 1f);
                        calmTimer = calmTime;
                    }
                }
                else
                {
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            if (dischargeTimer <= 0)
                            {
                                RevealWeakPoint(LADWeakPoint.Back);
                                Invoke("ActivateDischarge", 1f);
                                calmTimer = calmTime;
                            }
                            break;
                        case 1:
                            if (barrageTimer <= 0)
                            {
                                RevealWeakPoint(LADWeakPoint.Core);
                                StartBarrageCallBack?.Invoke();
                                Invoke("Barrage", 1f);
                                calmTimer = calmTime;
                            }
                            break;
                        case 2:
                            if (bombTimer <= 0)
                            {
                                RevealWeakPoint(LADWeakPoint.Core);
                                BombRainCallBack?.Invoke();
                                Invoke("BombRain", 1f);
                                calmTimer = calmTime;
                            }
                            break;
                        // TO DO : sbire summoning
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (bombTimer <= 0)
                {
                    RevealWeakPoint(LADWeakPoint.Core);
                    BombRainCallBack?.Invoke();
                    Invoke("BombRain", 1f);
                    calmTimer = calmTime;
                }
                // TO DO : sbire summoning
            }
        }
    }

    #endregion

    #region Discharge
    private void ActivateDischarge()
    {
        this.SwitchState(LADState.Discharge);
        dischargeRenderer.enabled = true;
        dischargeTimer = dischargeCooldown;
        Invoke("Discharge", dischargeChargeTime);
    }

    public void Discharge()
    {
        foreach (LifeSystem lifeSystem in ObjectsInDischarges)
        {
            lifeSystem.TakeDamage(dischargeDamage.damagesTypes, dischargeDamage.damages, gameObject);
        }
        dischargeRenderer.enabled = false;
        DischargeCallBack?.Invoke();
        this.SwitchState(LADState.Shoot);
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
    #endregion

    #region Barrage

    private void Barrage()
    {
        this.SwitchState(LADState.Barrage);
        Invoke("interruptBarrage", barrageTime);
        barrageTimer = barrageCooldown;
        rpmModifier = barrageRpmMultiplyer;
    }
    private void BarrageLogic()
    {
        TurnCore(barrageCoreRotationSpeed);
        TurnBarrels(barrageBarrelRotationSpeed);
        if (timeBeforeNextShot <= 0)
        {
            this.shootMultiple(5);
            timeBeforeNextShot = 60f / (rpm * rpmModifier);
        }
    }
    private void shootMultiple(int nbrToSpawn)
    {
        for (int i = 0; i < nbrToSpawn; i++)
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
            projectile.GetComponent<Rigidbody>().AddForce(barrels[currentBarrel].transform.right * -1 * 2000);
            //make a random between -spread and spread
            projectile.GetComponent<Rigidbody>().AddForce(barrels[currentBarrel].transform.up * 500 * Random.Range(-spread, spread));
        }
    }

    public void interruptBarrage()
    {
        EndBarrageCallBack?.Invoke();
        this.SwitchState(LADState.Shoot);
        rpmModifier = 1;
    }

    #endregion

    #region BombRain

    public void BombRain()
    {
        this.SwitchState(LADState.BombRain);
        GameObject bomb = Instantiate(bombPrefab, bombSpawnPoints[currentBombSpawnPoint].transform.position, bombSpawnPoints[currentBombSpawnPoint].transform.rotation);
        if (bomb.GetComponent<DamageData>())
        {
            bomb.GetComponent<DamageData>().damagesTypes = bombDamage.damagesTypes;
            bomb.GetComponent<DamageData>().damages = bombDamage.damages;
        }
        currentBombSpawnPoint++;
        if (currentBombSpawnPoint >= bombSpawnPoints.Count)
        {
            currentBombSpawnPoint = 0;
            this.SwitchState(LADState.Shoot);
            this.bombTimer = bombCooldown;
        }
        else
        {
            Invoke("BombRain", timeBetweenBombs);
        }
    }

    #endregion

    #region Call

    #endregion
}
