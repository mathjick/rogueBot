using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaBase : MonoBehaviour
{
    public EnnemieSpawnManager Spawner;

    virtual public void Dead()
    {
        if (Spawner) { Spawner.RemoveEnnemi(gameObject);}
        Invoke("Diseapear", 3);
    }

    public void Diseapear()
    {
        Destroy(gameObject);
    }
}
