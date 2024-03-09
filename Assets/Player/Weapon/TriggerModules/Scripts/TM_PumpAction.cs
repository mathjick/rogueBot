using UnityEngine;

public class TM_PumpAction : TriggerModule
{
    public bool isHolding = false;

    public override void Hold()
    {
        isHolding = true;
    }

    public override void Release()
    {
        isHolding = false;
    }
}
