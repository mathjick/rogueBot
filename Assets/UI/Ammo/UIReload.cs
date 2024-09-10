using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReload : MonoBehaviour
{
    [SerializeField] bool isReloading = false;
    float _timerMax;
    float _timer;
    Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (isReloading)
        {
            if (_timer < 0)
            {
                isReloading = false;
                _image.fillAmount = 1;
                UpdateDisplay();
            }
            else
            {
                _timer -= Time.deltaTime;
                UpdateDisplay();
            }
        }
    }


    public void UpdateReload(float timer)
    {
        isReloading = true;
        _timerMax = timer;
        _timer = _timerMax;
    }

    private void UpdateDisplay()
    {
        if (isReloading)
        {
            _image.enabled = true;
            _image.fillAmount = 1 - _timer / _timerMax;
        }
        else
        {
            _image.enabled = false;
        }
    }
}
