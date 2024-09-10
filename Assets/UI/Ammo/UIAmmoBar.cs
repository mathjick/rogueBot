using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAmmoBar : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;

    [SerializeField] private List<GameObject> ammoElements;

    [SerializeField] private GameObject parent;

    [SerializeField] private RectTransform parentTransform;
    [SerializeField] private float width;
    [SerializeField] private float spacing;

    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _maxAmmo;


    private void Awake()
    {
        ammoElements = new List<GameObject>();
        ResizeDisplay();
    }

    private void ResizeDisplay()
    {
        width = (parentTransform.sizeDelta.x - spacing * (_maxAmmo - 1)) / _maxAmmo;

        for (int i = 0; i < ammoElements.Count; i++)
        {
            Destroy(ammoElements[i]);
        }

        ammoElements.Clear();

        for (int i = 0; i < _maxAmmo; i++)
        {
            GameObject go = Instantiate(ammoPrefab, parentTransform);
            ammoElements.Add(go);
            go.transform.SetParent(this.transform);
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(width, go.GetComponent<RectTransform>().sizeDelta.y);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * width + (i - 1) * spacing, 0, 0);
        }
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < _maxAmmo; i++)
        {
            if (_maxAmmo - i <= _currentAmmo)
            {
                ammoElements[i].SetActive(true);
            }
            else
            {
                ammoElements[i].SetActive(false);
            }
        }
    }

    public void UpdateAmmoMax(int maxAmmo)
    {
        _maxAmmo = maxAmmo;

        ResizeDisplay();
    }

    public void UpdateAmmoBar(int currentAmmo)
    {
        _currentAmmo = currentAmmo;

        UpdateDisplay();
    }
}
