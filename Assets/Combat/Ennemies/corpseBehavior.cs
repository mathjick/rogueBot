using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corpseBehavior : MonoBehaviour
{
    public LifeSystem ls;

    public void deathLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Corpse");
    }
}
