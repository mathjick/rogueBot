using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZone : MonoBehaviour
{
    public float TickRate;
    public DamageData damageData;

    private List<PlayerController> playersInZone = new List<PlayerController>();
    private float timer = 0;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= TickRate)
        {
            foreach (PlayerController player in playersInZone)
            {
                player.lifeSystem.TakeDamage(damageData.damagesTypes,damageData.damages,this.gameObject);
            }
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tag_player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!playersInZone.Contains(player))
            {
                playersInZone.Add(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("tag_player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (playersInZone.Contains(player))
            {
                playersInZone.Remove(player);
            }
        }
    }
}
