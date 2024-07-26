using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset fontAsset;
    [SerializeField] private FontAssetScriptableEvent fontAssetEvent;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            fontAssetEvent.Trigger(fontAsset);
        }
    }
}
