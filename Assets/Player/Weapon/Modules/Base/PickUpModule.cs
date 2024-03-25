using UnityEngine;

public class PickUpModule : MonoBehaviour
{
    public BaseModule moduleToAddToInventory;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventory>())
        {
            other.GetComponent<PlayerInventory>();
            if (!other.GetComponent<PlayerInventory>().TryAddModule(moduleToAddToInventory))
            {
                // to do
            }
            else
            {
                moduleToAddToInventory.transform.SetParent(other.transform);
                Destroy(gameObject);
            }
        }
    }
}