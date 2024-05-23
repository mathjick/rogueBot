using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMineMelee : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject playerToFocus;

    public LifeSystem ls;

    private void Start()
    {
        if (PlayerController.instance)
        {
            playerToFocus = PlayerController.instance.gameObject;
        }
        agent.updateRotation = true;
    }

    private void Update()
    {
        /**if (playerToFocus == null)
        {
            if (PlayerController.instance)
            {
                playerToFocus = PlayerController.instance.gameObject;
            }
        }*/
        if (ls.lifePoints > 0)
        {
            Move();
        }
        else
        {
            agent.Stop();
        }
    }

    private void FixedUpdate()
    {
               
    }

    public void Move()
    {
        agent.SetDestination(playerToFocus.transform.position);
    }
}
