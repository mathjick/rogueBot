using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmoText : MonoBehaviour
{
    [SerializeField] private AnimationCurve _popCurve;
    [SerializeField] private float _timer;
    [SerializeField] private float _timerLimit;
    [SerializeField] private bool _isPlayingAnimation;

    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private float _originalFontSize;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _timerLimit = _popCurve[_popCurve.length - 1].time;
        _originalFontSize = _tmp.fontSize;
    }

    private void Update()
    {
        if (_isPlayingAnimation)
        {
            _timer += Time.deltaTime;

            _tmp.fontSize = _originalFontSize * _popCurve.Evaluate(_timer);

            if (_timer > _timerLimit)
            {
                _isPlayingAnimation = false;
            }
        }
    }

    public void UpdateAmmo(int currentAmmo)
    {
        _tmp.text = currentAmmo.ToString();
        _timer = 0f;
        _isPlayingAnimation = true;
        Debug.Log(_isPlayingAnimation);
    }
}
