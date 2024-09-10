using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericScriptableEventListener<T> : MonoBehaviour
{
    public GenericScriptableEvent<T> scriptableEvent;
    public UnityEvent<T> response;

    public void OnEnable()
    {
        scriptableEvent.AddListener(this);
    }

    public void OnDisable()
    {
        scriptableEvent.RemoveListener(this);
    }

    public void OnEventTrigger(T value)
    {
        response.Invoke(value);
    }
}
