using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericScriptableEvent<T> : ScriptableObject
{
    private List<GenericScriptableEventListener<T>> listeners = new List<GenericScriptableEventListener<T>>();

    public void AddListener(GenericScriptableEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GenericScriptableEventListener<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Trigger(T value)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventTrigger(value);
        }
    }
}
