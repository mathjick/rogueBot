using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private int _maxHealth = 1;

    [SerializeField] private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateHealth(int health)
    {
        _health = health;
        UpdateDisplay();
    }

    public void UpdateMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _image.fillAmount = (float) _health / (float) _maxHealth;
    }
}
