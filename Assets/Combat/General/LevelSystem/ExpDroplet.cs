using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDroplet : MonoBehaviour
{
    public int expAmount = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().levelSystem.AddExp(expAmount);
            Destroy(gameObject);
        }
    }
}
