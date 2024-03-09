using UnityEngine;

public class TM_Marksman : TriggerModule
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
