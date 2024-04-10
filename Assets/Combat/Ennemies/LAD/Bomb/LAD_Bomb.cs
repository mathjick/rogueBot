using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAD_Bomb : MonoBehaviour
{
    public float fuzeTime = 5;
    private float fuzeTimer;
    // Start is called before the first frame update
    void Start()
    {
        fuzeTimer = fuzeTime;
    }

    public void Update()
    {
        fuzeTimer -= Time.deltaTime;
        if (fuzeTimer <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
