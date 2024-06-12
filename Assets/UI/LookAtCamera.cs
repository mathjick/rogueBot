using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtusedCamera : MonoBehaviour
{

    public Camera usedCamera;
    public Transform objectToRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (usedCamera == null)
        {
            usedCamera = FindFirstObjectByType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (usedCamera)
        {
            Vector3 target = new Vector3(
                objectToRotate.position.x - (usedCamera.transform.position.x - objectToRotate.position.x),
                objectToRotate.transform.position.y,
                objectToRotate.position.z - (usedCamera.transform.position.z - objectToRotate.position.z));
            objectToRotate.LookAt(target);
            //objectToRotate.rotation = Quaternion.LookRotation()
        }
        else
        {
            if (PlayerController.instance)
            {
                usedCamera = PlayerController.instance.playerView;
            }
        }
    }
}
