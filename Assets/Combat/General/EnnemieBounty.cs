using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Bounty
{
    public GameObject bounty;
    [Range(0, 1)]
    public float chanceToDrop;
}
public class EnnemieBounty : MonoBehaviour
{
    public List<Bounty> bountyList;

    public void DropBounty()
    {
        foreach (Bounty bounty in bountyList)
        {
            if (Random.value <= bounty.chanceToDrop)
            {
                Instantiate(bounty.bounty, transform.position, Quaternion.identity);
            }
        }
    }
}
