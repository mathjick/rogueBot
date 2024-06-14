using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIAmmoDisplayManager : MonoBehaviour
{
    public static UIAmmoDisplayManager instance;

    [SerializeField] private PlayerWeapon weapon;
    [SerializeField] private float displayThreshold;

    [SerializeField] private UIReloadText reloadText;
    [SerializeField] private float currentRatio = 1f;

    [SerializeField] private UIAmmoDisplay ammoDisplay;

    private bool reloadDisplayed = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        reloadText = GetComponentInChildren<UIReloadText>();
        ammoDisplay = GetComponentInChildren<UIAmmoDisplay>();
    }
    private void Update()
    {
        if (weapon.inventory.triggerModuleEquipped)
        {
            currentRatio = (float) weapon.inventory.triggerModuleEquipped.actualNumberOfRounds / (float) weapon.inventory.triggerModuleEquipped.RoundsPerMag;

        }

        if (!reloadDisplayed)
        {
            if (currentRatio <= displayThreshold)
            {
                reloadText.DisplayText();
                reloadDisplayed = true;
            }
        }
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        ammoDisplay.UpdateAmmoDisplay(currentAmmo);
    }

    public void HideReloadDisplay()
    {
        reloadText.HideText();
        reloadDisplayed = false;
    }
}
