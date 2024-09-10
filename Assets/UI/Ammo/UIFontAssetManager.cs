using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(FontAssetScriptableEventListener))]
public class UIFontAssetManager : MonoBehaviour
{
    private TextMeshProUGUI _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    public void SetFontAsset(TMP_FontAsset fontAsset)
    {
        _tmp.font = fontAsset;
        _tmp.fontSharedMaterial = fontAsset.material;
    }
}
