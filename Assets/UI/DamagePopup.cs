using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable] public struct FontParameters
{
    public Color fontColor;
    public int fontSize;
}

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 pos, float damageAmount, bool isCrit)
    {
        GameObject go = Instantiate(GameAssets.instance.damagePopupPrefab, pos, Quaternion.identity);
        DamagePopup damagePopup = go.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCrit);

        return damagePopup;
    }

    private static int sortingOrder;

    [Space(1)]
    [Header("Behavior Setup")]
    [Space(1)]
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private float moveVectorDecreaseRatio;
    [SerializeField] private float vanishTimeMax;
    [SerializeField] private float vanishSpeed;
    
    [Space(1)]
    [Header("Color Setup")]
    [Space(1)]
    [SerializeField] private FontParameters normalDamage;
    [SerializeField] private FontParameters critDamage;

    private TextMeshPro textMesh;
    private Color textColor;

    private float vanishTimer;

    private void Awake()
    {
        textMesh = this.GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount, bool isCrit)
    {
        Debug.Log(transform.position);
        if (!isCrit)
        {
            textMesh.fontSize = normalDamage.fontSize;
            textColor = normalDamage.fontColor;
        }
        else
        {
            textMesh.fontSize = critDamage.fontSize;
            textColor = critDamage.fontColor;
        }

        textMesh.SetText(damageAmount.ToString());
        textMesh.color = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        vanishTimer = vanishTimeMax;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * moveVectorDecreaseRatio * Time.deltaTime;

        vanishTimer -= Time.deltaTime;

        if (vanishTimer > 0)
        {
            if (vanishTimer > vanishTimeMax / 2f)
            {
                float increaseScaleAmount = 1f;
                transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            }
            else
            {
                float decreaseScaleAmount = 1f;
                transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
            }
        }
        else
        {
            textColor.a -= vanishSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
