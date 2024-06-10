using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject damagePopupPrefab;

    private void Start()
    {
        //DamagePopup.Create(Vector3.zero, 300);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isCrit = Random.Range(0, 100) < 30;
            DamagePopup.Create(Input.mousePosition, 300, isCrit);
        }
    }
}
