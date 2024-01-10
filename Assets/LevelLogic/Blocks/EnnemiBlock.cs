using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiBlock : BlockLogic
{
    public override void OnGeneration()
    {
        Debug.Log("Generation");
    }

    public override void OnPlayerEnter()
    {
        Debug.Log("PlayerEnter");
    }

    public override void OnPlayerExit()
    {
        Debug.Log("PlayerExit");
    }

    public override void OnPlayerFinished()
    {
        Debug.Log("PlayerFinished");
    }
}
