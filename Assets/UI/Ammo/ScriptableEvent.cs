using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Event/Scriptable Event")]
public class ScriptableEvent : ScriptableObject
{
    private List<ScriptableEventListener> listeners;

    public void AddListener(ScriptableEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(ScriptableEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Trigger()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventTrigger();
        }
    }
}
