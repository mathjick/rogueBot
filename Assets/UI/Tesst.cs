using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesst : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("F10");
            if (UIData.instance.currentFontAsset != UIData.instance.standardFontAsset)
                UIData.instance.currentFontAsset = UIData.instance.standardFontAsset;
            else
                UIData.instance.currentFontAsset = UIData.instance.accessibleFontAsset;
            UIData.OnLanguageChanged.Invoke();
        }
    }
}
