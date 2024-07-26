using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReload : MonoBehaviour
{
    bool isReloading = false;
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
            if (_timerMax < 0)
            {
                isReloading = false;
                _image.fillAmount = 0;
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
        _image.fillAmount = _timer / _timerMax;
    }
}
