using UnityEngine;

public class PickUpTriggerModule : MonoBehaviour
{
    public TriggerModule triggerModuleToEquip;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>())
        {
            other.GetComponent<PlayerInventory>().triggerModuleEquipped = triggerModuleToEquip;
            UIAmmoDisplayManager.instance.UpdateAmmoDisplay(triggerModuleToEquip.actualNumberOfRounds);
            triggerModuleToEquip.playerWeapon = other.GetComponent<PlayerInventory>().weapon;
            triggerModuleToEquip.transform.SetParent(other.transform);
            triggerModuleToEquip.weaponView = other.GetComponent<PlayerInventory>().playerController.playerCameraSystem.weaponView.GetComponent<Camera>();
            Destroy(gameObject);
        }
    }
}