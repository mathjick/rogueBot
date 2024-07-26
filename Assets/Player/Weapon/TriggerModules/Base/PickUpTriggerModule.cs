using UnityEngine;

public class PickUpTriggerModule : MonoBehaviour
{
    public TriggerModule triggerModuleToEquip;

    [SerializeField] private IntScriptableEvent updateAmmoMax;
    [SerializeField] private IntScriptableEvent updateAmmo;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>())
        {
            other.GetComponent<PlayerInventory>().triggerModuleEquipped = triggerModuleToEquip;
            updateAmmoMax.Trigger(triggerModuleToEquip.RoundsPerMag);
            updateAmmo.Trigger(triggerModuleToEquip.RoundsPerMag);
            triggerModuleToEquip.playerWeapon = other.GetComponent<PlayerInventory>().weapon;
            triggerModuleToEquip.transform.SetParent(other.transform);
            triggerModuleToEquip.weaponView = other.GetComponent<PlayerInventory>().playerController.playerCameraSystem.weaponView.GetComponent<Camera>();
            Destroy(gameObject);
        }
    }
}