using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAnimation : MonoBehaviour
{
    [SerializeField] private Transform _structure;
    [SerializeField] private Transform _core;

    [SerializeField] private float _rotationSpeed;


    void Update()
    {
        _structure.Rotate(0f, _rotationSpeed * Time.deltaTime, _rotationSpeed * Time.deltaTime / 2, Space.Self);
        _core.Rotate(_rotationSpeed * Time.deltaTime * -1, 0f, _rotationSpeed * Time.deltaTime / 2, Space.Self);
    }
}
