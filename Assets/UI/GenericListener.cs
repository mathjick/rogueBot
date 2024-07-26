using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericListener<T> : MonoBehaviour
{
    public GenericEvent<T> genericEvent;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        genericEvent.AddListener(this);
    }

    private void OnDisable()
    {
        genericEvent.RemoveListener(this);
    }

    public void OnEventTrigger(T value)
    {
        response.Invoke(value);
    }
}
