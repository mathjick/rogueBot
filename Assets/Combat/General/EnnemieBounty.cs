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

    public int expValue;
    public GameObject expOrb;

    public void DropBounty()
    {
        foreach (Bounty bounty in bountyList)
        {
            if (Random.value <= bounty.chanceToDrop)
            {
                Instantiate(bounty.bounty, transform.position, Quaternion.identity);
            }
        }

        for (int i = 0; i < expValue; i++)
        {
            Vector3 randPosition = new Vector3(transform.position.x + Random.value * 3, transform.position.y + Random.value * 3, transform.position.z + Random.value * 3);
            Instantiate(expOrb, randPosition, Quaternion.identity);
        }
    }
}
