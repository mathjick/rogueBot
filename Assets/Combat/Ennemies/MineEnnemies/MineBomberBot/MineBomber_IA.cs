using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public enum MineBomber_States
{
    Idle,Move,Attack
}
public class MineBomber_IA : MonoBehaviour
{
    [Header("--- setup ---")]
    //logic
    public MineBomber_States state;
    public LifeSystem lifeSystem;
    public GameObject projectileToShoot;
    public GameObject projectileSpawnPoint;
    public GameObject canonAnchor;
    public NavMeshAgent navMeshAgent;

    [Space(2)]
    //feedbacks
    public GameObject spawnEffect;
    public GameObject deathEffect;

    [Header("--- parameters ---")]
    public float maxAttackRange;
    public float minAttackRange;
    public float attackRate;
    public float attackWarmup;

    public GameObject playerToFocus;

    private float attackTimer;

    public void Start()
    {
        if (PlayerController.instance)
        {
            playerToFocus = PlayerController.instance.gameObject;
        }
        navMeshAgent.updateRotation = true;
        attackTimer = attackRate;
    }

    public void Update()
    {
        if (playerToFocus)
        {
            float distance = Vector3.Distance(playerToFocus.transform.position, this.transform.position);
            if (distance < minAttackRange || distance > maxAttackRange)
            {
                ChangeState(MineBomber_States.Move);
            }
            else
            {
                ChangeState(MineBomber_States.Attack);
            }
        }
        else
        {
            ChangeState(MineBomber_States.Idle);
        }
    }

    public void FixedUpdate()
    {
        switch (this.state)
        {
            case MineBomber_States.Idle:
                break;
            case MineBomber_States.Move:
                break;
            case MineBomber_States.Attack:
                if(attackTimer > 0) { attackTimer -= Time.deltaTime; }
                Attack();
                break;
        }
    }

    public void Move()
    {
        float distance = Vector3.Distance(playerToFocus.transform.position, this.transform.position);
        if (distance > maxAttackRange)
        {
            navMeshAgent.SetDestination(playerToFocus.transform.position);
        }
        if (distance < minAttackRange)
        {
            NavMeshHit hit;
            Vector3 oppositeSide = transform.position - (playerToFocus.transform.position - transform.position);
            NavMesh.SamplePosition(oppositeSide, out hit, 100, 1);
            navMeshAgent.SetDestination(hit.position);
        }
    }
    public void StopMoving()
    {
        navMeshAgent.SetDestination(this.transform.position);
    }
    public void Attack()
    {
        //turn the ennemy to the player
        Vector3 direction = playerToFocus.transform.position - this.transform.position;
        direction.y = direction.y + 50;
        this.transform.rotation = Quaternion.LookRotation(direction);
        //shoot
        if (attackTimer <= 0)
        {
            GameObject _newProjectile = Instantiate(projectileToShoot, projectileSpawnPoint.transform.position, canonAnchor.transform.rotation);
            _newProjectile.GetComponent<Rigidbody>().AddForce(canonAnchor.transform.right * 1000);
            attackTimer = attackRate;
        }
    }

    public void ChangeState(MineBomber_States _state)
    {
        this.state = _state;
        switch (_state)
        {
            case MineBomber_States.Idle:
                StopMoving();
                break;
            case MineBomber_States.Move:
                Move();
                break;
            case MineBomber_States.Attack:
                StopMoving();
                break;
        }
    }
}
