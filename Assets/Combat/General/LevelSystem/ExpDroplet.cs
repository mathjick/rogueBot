using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDroplet : MonoBehaviour
{
    public int expAmount = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tag_player"))
        {
            other.GetComponent<PlayerController>().levelSystem.AddExp(expAmount);
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("tag_solid"))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
