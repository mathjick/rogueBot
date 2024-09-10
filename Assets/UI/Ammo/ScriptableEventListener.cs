using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    public ScriptableEvent scriptableEvent;
    public UnityEvent response;

    public void OnEnable()
    {
        scriptableEvent.AddListener(this);
    }

    public void OnDisable()
    {
        scriptableEvent.RemoveListener(this);
    }

    public void OnEventTrigger()
    {
        response.Invoke();
    }
}
