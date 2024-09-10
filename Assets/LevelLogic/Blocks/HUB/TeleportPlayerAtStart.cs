using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LevelGenerator.instance)
        {
            LevelGenerator.instance.player.transform.position = transform.position;
        }
    }
}
