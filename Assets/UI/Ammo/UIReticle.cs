using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReticle : MonoBehaviour
{

    [SerializeField] private Image _reticle;

    public void UpdateReticle(Sprite reticle)
    {
        _reticle.sprite = reticle;
    }
}
