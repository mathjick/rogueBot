using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIReloadText : MonoBehaviour
{
    [SerializeField] private AnimationCurve glowCurve;
    [SerializeField] private Material matA;
    [SerializeField] private Material matB;


    [SerializeField] private TextMeshProUGUI tmp;

    [SerializeField] private float timer;
    [SerializeField] private float timerLimit;
    [SerializeField] private float evaluation;

    private void Awake()
    {
        timerLimit = glowCurve[glowCurve.length - 1].time;
        tmp = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        if (isActiveAndEnabled)
        {
            //tmp.fontSharedMaterial.SetFloat("_GlowPower", glowCurve.Evaluate(timer));
            evaluation = glowCurve.Evaluate(timer);
            //tmp.fontSharedMaterial.Lerp(matA, matB, evaluation);
            tmp.fontMaterial.Lerp(matA, matB, evaluation);
            timer += Time.deltaTime;

            if (timer > timerLimit)
            {
                timer = 0f;
            }
        }
    }

    public void DisplayText()
    {
        timer = 0f;
        Debug.Log("display");
        tmp.enabled = true;
    }

    public void HideText()
    {
        Debug.Log("hide");
        tmp.enabled = false;
    }
}
