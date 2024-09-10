using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEvent<T> : ScriptableObject
{
    private List<GenericListener<T>> listeners;
    
    public void AddListener(GenericListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GenericListener<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Trigger(T value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTrigger(value);
        }
    }
}
