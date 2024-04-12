using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 originPosition;
    public Quaternion originRotation;

    public float decayFactor;
    public float shakeIntensity;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeIntensity > 0)
        {
            transform.localPosition = new Vector3(
                originPosition.x + Random.Range(-shakeIntensity, shakeIntensity) * 0.02f,
                originPosition.y + Random.Range(-shakeIntensity, shakeIntensity) * 0.02f, 
                transform.localPosition.z - Time.deltaTime * decayFactor);

            shakeIntensity -= Time.deltaTime * decayFactor;
        }
    }

    public void Shake()
    {
        shakeIntensity = 0.2f;
        decayFactor = .4f;
        transform.localPosition = new Vector3(
            originPosition.x + Random.Range(-shakeIntensity, shakeIntensity) * 0.02f,
            originPosition.y + Random.Range(-shakeIntensity, shakeIntensity) * 0.02f,
            originPosition.z + shakeIntensity);
    }
}
