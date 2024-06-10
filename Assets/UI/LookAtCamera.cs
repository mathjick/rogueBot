using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Camera camera;
    public Transform objectToRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null)
        {
            camera = FindFirstObjectByType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camera)
        {
            Vector3 target = new Vector3(
                objectToRotate.position.x - (camera.transform.position.x - objectToRotate.position.x),
                objectToRotate.transform.position.y,
                objectToRotate.position.z - (camera.transform.position.z - objectToRotate.position.z));
            objectToRotate.LookAt(target);
            //objectToRotate.rotation = Quaternion.LookRotation()
        }
        else
        {
            if (PlayerController.instance)
            {
                camera = PlayerController.instance.playerView;
            }
        }
    }
}
