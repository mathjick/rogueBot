using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIFontManager : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();

        //SetFont();
        //UIData.OnLanguageChanged.AddListener(SetFont);
    }

    public void SetFont(TMP_FontAsset font)
    {
        tmp.font = font;
        tmp.fontSharedMaterial = font.material;
    }
}
