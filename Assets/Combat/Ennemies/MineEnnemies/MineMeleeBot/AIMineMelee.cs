using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMineMelee : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject playerToFocus;

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

        Move();
    }

    private void FixedUpdate()
    {
               
    }

    public void Move()
    {
        agent.SetDestination(playerToFocus.transform.position);
    }
}
