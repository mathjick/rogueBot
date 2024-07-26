using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIData : MonoBehaviour
{
    public static UIData instance;
    public TMP_FontAsset standardFontAsset;
    public TMP_FontAsset accessibleFontAsset;
    public TMP_FontAsset currentFontAsset;



    private static UnityEvent _OnFontChanged;
    public static UnityEvent OnLanguageChanged
    {
        get
        {
            Debug.Log("on Language Changed");
            if (_OnFontChanged == null)
                _OnFontChanged = new UnityEvent();
            return _OnFontChanged;
        }
    }

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
    }
}
