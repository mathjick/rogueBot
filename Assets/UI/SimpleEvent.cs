using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEvent : ScriptableObject
{
    private List<SimpleListener> listeners;

    public void AddListener(SimpleListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(SimpleListener listener)
    {
        listeners.Remove(listener);
    }

    public void Trigger()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTrigger();
        }
    }
}
