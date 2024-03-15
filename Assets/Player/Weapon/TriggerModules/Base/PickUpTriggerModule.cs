using UnityEngine;

public class PickUpTriggerModule : MonoBehaviour
{
    public TriggerModule triggerModuleToEquip;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>())
        {
            other.GetComponent<PlayerInventory>().triggerModuleEquipped = triggerModuleToEquip;
            triggerModuleToEquip.playerWeapon = other.GetComponent<PlayerInventory>().weapon;
            triggerModuleToEquip.transform.SetParent(other.transform);
            Destroy(gameObject);
        }
    }
}