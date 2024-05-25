using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionModuleStates
{
    Up,
    Down,
    Left,
    Right,
    Front,
    Back
}

[System.Serializable]
public struct directionBlocked
{
    public DirectionModuleStates direction;
    public bool isBlocked;
}

public class DirectionSystem : MonoBehaviour
{
    public List<directionBlocked> directionModules = new List<directionBlocked>();
    public List<DirectionModule> proximityModules = new List<DirectionModule>();

    public void Start()
    {
        UpdateProximityList();
    }

    public void UpdateProximityList()
    {
        directionModules.Clear();
        foreach (DirectionModule module in proximityModules)
        {
            directionBlocked _newModule =  new directionBlocked { direction = module.direction, isBlocked = false};
            foreach (Collider collider in module.collidersIn)
            {
                if (collider == null && collider.gameObject.tag == "tag_solid")
                {
                    _newModule.isBlocked = true;
                }
            }
            directionModules.Add(_newModule);
        }
    }

    public bool CheckDirection(DirectionModuleStates direction)
    {
        foreach (directionBlocked module in directionModules)
        {
            if (module.direction == direction)
            {
                return module.isBlocked;
            }
        }
        return false;
    }
}
