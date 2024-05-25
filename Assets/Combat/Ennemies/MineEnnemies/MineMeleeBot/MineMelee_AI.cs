using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MineMelee_States
{
    Idle, Move, Attack, Dead, Jump
}

public class MineMelee_AI : IaBase
{
    [Header("--- setup ---")]
    //logic
    public MineMelee_States state;
    public NavMeshAgent agent;
    public GameObject playerToFocus;
    public LifeSystem ls;
    public TriggerRelay damageTrigger;

    private float baseSpeed;
    private float attackTimer;
    private List<LifeSystem> lifeSystemsInAttackRange = new List<LifeSystem>();

    [Space(2)]
    //feedbacks
    public GameObject spawnEffect;
    public GameObject deathEffect;

    [Header("--- parameters ---")]
    public float jumpMaxRange;
    public float jumpMinRange;
    public DamageData damageData;
    public float attackRate;
    public float speedMultiplyer;

    private void Start()
    {
        baseSpeed = agent.speed;
        if (PlayerController.instance)
        {
            playerToFocus = PlayerController.instance.gameObject;
        }
        agent.updateRotation = true;
        attackTimer = attackRate;
    }

    private void Update()
    {
        if (state != MineMelee_States.Dead)
        {
            if (playerToFocus && state != MineMelee_States.Jump)
            {
                ChangeState(MineMelee_States.Move);
            }
        }

    }

    private void FixedUpdate()
    {
        if(playerToFocus && Vector3.Distance(playerToFocus.transform.position,this.transform.position) < jumpMaxRange && Vector3.Distance(playerToFocus.transform.position, this.transform.position) > jumpMinRange)
        {
            ChangeState(MineMelee_States.Jump);
        }
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackRate;
            foreach (LifeSystem ls in lifeSystemsInAttackRange)
            {
                ls.TakeDamage(damageData.damagesTypes,damageData.damages,this.gameObject);
            }
        }
    }

    public void Move()
    {
        if (state != MineMelee_States.Dead)
        {
            agent.isStopped = false;
            agent.SetDestination(playerToFocus.transform.position);
        }
    }

    public void Jump()
    {
        if (state != MineMelee_States.Dead)
        {
            agent.speed = speedMultiplyer * baseSpeed;
            Invoke("ChangeToMove", 1f);
        }
    }

    public void ChangeState(MineMelee_States _newState)
    {
        state = _newState;
        switch (_newState)
        {
            case MineMelee_States.Idle:
                break;
            case MineMelee_States.Move:
                Move();
                break;
            case MineMelee_States.Attack:
                break;
            case MineMelee_States.Dead:
                break;
            case MineMelee_States.Jump:
                Jump();
                break;
        }
    }

    public void ChangeToMove()
    {
        if (state != MineMelee_States.Dead)
        {
            agent.speed = baseSpeed;
            ChangeState(MineMelee_States.Move);
        }
    }

    public void UpdateLifesystemInTrigger()
    {
        lifeSystemsInAttackRange.Clear();
        foreach(Collider col in damageTrigger.collidersIn)
        {
            lifeSystemsInAttackRange.Add(col.GetComponent<LifeSystem>());
        }
    }

    override public void Dead()
    {
        ChangeState(MineMelee_States.Dead);
        base.Dead();
    }
}
