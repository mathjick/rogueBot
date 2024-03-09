using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerModule : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public virtual void Hold()
    {
        Debug.Log("Hold with module");
    }

    public virtual void Release()
    {
        Debug.Log("Release with module");
    }
}
