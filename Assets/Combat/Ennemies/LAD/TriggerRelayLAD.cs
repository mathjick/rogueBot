using UnityEngine;
using UnityEngine.Events;

public enum TriggerType
{
    DischargeZone
}

public class TriggerRelayLAD : MonoBehaviour
{
    public TriggerType triggerType;
    public IA_LAD lad;
    private void OnTriggerEnter(Collider other)
    {
        switch (triggerType)
        {
            case TriggerType.DischargeZone:
                lad.EnterDischargeZone(other);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (triggerType)
        {
            case TriggerType.DischargeZone:
                lad.ExitDischargeZone(other);
                break;
            default:
                break;
        }
    }
}
