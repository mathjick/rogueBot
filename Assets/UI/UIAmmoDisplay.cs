using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmoDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();       
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        tmp.text = currentAmmo.ToString() + " / -";
    }
}
