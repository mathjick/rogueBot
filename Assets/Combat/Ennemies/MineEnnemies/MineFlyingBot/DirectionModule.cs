using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionModule : MonoBehaviour
{
    public DirectionModuleStates direction;
    public List<Collider> collidersIn = new List<Collider>();
    public DirectionSystem directionSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (!collidersIn.Contains(other))
        {
            collidersIn.Add(other);
        }
        directionSystem.UpdateProximityList();
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidersIn.Contains(other))
        {
            collidersIn.Remove(other);
        }
        directionSystem.UpdateProximityList();
    }
}
