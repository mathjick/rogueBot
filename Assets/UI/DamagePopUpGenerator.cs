using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator instance;

    public GameObject damagePopUpPrefab;

    public FontParameters normalFont;
    public FontParameters critFont;

    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;

    private void Awake()
    {
        if (instance != null && instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            CreatePopUp(Vector3.one * 2, Random.Range(0, 1000).ToString(), true);
        }
    }

    public void CreatePopUp(Vector3 position, string text, bool isCrit)
    {
        var popup = Instantiate(damagePopUpPrefab, position + new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        if(isCrit)
        {
            temp.font = critFont.font;
            temp.fontSize = critFont.fontSize;
            temp.faceColor = critFont.fontColor;
        }
        else
        {
            temp.font = normalFont.font;
            temp.fontSize = normalFont.fontSize;
            temp.faceColor = normalFont.fontColor;
        }

        Destroy(popup, 1f);
    }
}
