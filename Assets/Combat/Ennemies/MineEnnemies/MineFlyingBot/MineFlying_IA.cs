using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MineFlying_States
{
    Idle, MoveClose, MoveAway, Attack, Dead
}

public class MineFlying_IA : IaBase
{
    [Header("--- setup ---")]
    //logic
    public MineFlying_States state;
    public LifeSystem lifeSystem;
    public Rigidbody rb;
    public DirectionSystem directionSystem;
    public GameObject projectileToShoot;
    public GameObject projectileSpawnPoint;

    [Space(2)]
    //feedbacks
    public GameObject spawnEffect;
    public GameObject deathEffect;

    [Header("--- parameters ---")]
    public float maxAttackRange;
    public float minAttackRange;
    public float attackRate;
    public float moveSpeed;
    [Range(0, 1)]
    public float randomFactor;

    public GameObject playerToFocus;

    private float attackTimer;

    public void Start()
    {
        ChangeState(MineFlying_States.MoveClose);
        attackTimer = attackRate;
    }

    public void Update()
    {
        if (state != MineFlying_States.Dead)
        {
            if (playerToFocus && state != MineFlying_States.MoveClose && Vector3.Distance(playerToFocus.transform.position, transform.position) > maxAttackRange)
            {
                ChangeState(MineFlying_States.MoveClose);
            }
            if (playerToFocus && state != MineFlying_States.MoveClose && Vector3.Distance(playerToFocus.transform.position, transform.position) < minAttackRange)
            {
                ChangeState(MineFlying_States.MoveAway);
            }
            if (playerToFocus && state != MineFlying_States.Attack && Vector3.Distance(playerToFocus.transform.position, transform.position) < maxAttackRange && Vector3.Distance(playerToFocus.transform.position, transform.position) > minAttackRange)
            {
                ChangeState(MineFlying_States.Attack);
            }
        }
    }

    public void FixedUpdate()
    {
        if (state != MineFlying_States.Dead)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 2);
            if (playerToFocus)
            {
                gameObject.transform.LookAt(playerToFocus.transform);
            }
            switch (state)
            {
                case MineFlying_States.Idle:
                    break;
                case MineFlying_States.MoveClose:
                    Move(Time.deltaTime);
                    break;
                case MineFlying_States.MoveAway:
                    Move(Time.deltaTime);
                    Attack(Time.deltaTime);
                    break;
                case MineFlying_States.Attack:
                    Attack(Time.deltaTime);
                    break;
            }
        }
    }

    public void Move(float _deltaTime)
    {
        if (state != MineFlying_States.Dead)
        {
            if (playerToFocus)
            {
                Vector3 baseDirection = (playerToFocus.transform.position - transform.position);
                if (state == MineFlying_States.MoveAway)
                {
                    baseDirection *= -1;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Up) && baseDirection.y > 0)
                {
                    baseDirection.y = 0;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Down) && baseDirection.y < 0)
                {
                    baseDirection.y = 0;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Left) && baseDirection.x < 0)
                {
                    baseDirection.x = 0;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Right) && baseDirection.x > 0)
                {
                    baseDirection.x = 0;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Front) && baseDirection.z > 0)
                {
                    baseDirection.z = 0;
                }
                if (directionSystem.CheckDirection(DirectionModuleStates.Back) && baseDirection.z < 0)
                {
                    baseDirection.z = 0;
                }
                rb.velocity = baseDirection.normalized * moveSpeed;
            }
        }
    }

    public void Attack(float _deltaTime)
    {
        if (state != MineFlying_States.Dead)
        {
            if (attackTimer <= 0)
            {
                GameObject _projectile = Instantiate(projectileToShoot, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
                Vector3 _predictedVector = ((playerToFocus.transform.position + (playerToFocus.GetComponent<Rigidbody>().velocity * 0.8f)) - transform.position).normalized * 2000;
                Vector3 _randomVector = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
                _randomVector = _randomVector.normalized * 2000;

                _projectile.GetComponent<Rigidbody>().AddForce(Vector3.Lerp(_predictedVector, _randomVector, randomFactor));
                attackTimer = attackRate;
            }
            else
            {
                attackTimer -= _deltaTime;
            }
        }
    }

    public void ChangeState(MineFlying_States _state)
    {
        if (state != MineFlying_States.Dead)
        {
            this.state = _state;
            switch (_state)
            {
                case MineFlying_States.Idle:
                    break;
                case MineFlying_States.MoveClose:
                    break;
                case MineFlying_States.MoveAway:
                    break;
                case MineFlying_States.Attack:
                    break;
            }
        }
    }

    override public void Dead()
    {
        ChangeState(MineFlying_States.Dead);
        rb.useGravity = true;
        base.Dead();
    }
}
