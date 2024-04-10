using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerRelay : MonoBehaviour
{
    //make a unity event and invoke it when the trigger is entered
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public List<Collider> collidersIn;

    private void OnTriggerEnter(Collider other)
    {
        if (!collidersIn.Contains(other))
        {
            collidersIn.Add(other);
        }
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidersIn.Contains(other))
        {
            collidersIn.Remove(other);
        }
        onTriggerExit.Invoke();
    }
}
