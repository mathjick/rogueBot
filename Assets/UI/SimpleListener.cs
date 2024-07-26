using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleListener : MonoBehaviour
{
    public SimpleEvent simpleEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        simpleEvent.AddListener(this);
    }

    private void OnDisable()
    {
        simpleEvent.RemoveListener(this);
    }

    public void OnEventTrigger()
    {
        response.Invoke();
    }
}
