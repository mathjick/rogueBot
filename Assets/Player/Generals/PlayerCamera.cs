using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public PlayerController playerController;
    public Vector2 _lookMultiplyer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(InputValue val)
    {
        if(Mathf.Abs(val.Get<Vector2>().x) <= 5 && Mathf.Abs(val.Get<Vector2>().y) <= 5)
        {
            Debug.Log("x : " + val.Get<Vector2>().x + ", y : " + val.Get<Vector2>().y);
        }
        else
        {
            playerController.playerTransform.Rotate(new Vector3(0, val.Get<Vector2>().x * _lookMultiplyer.x, 0));
            playerController.playerView.transform.Rotate(new Vector3(-val.Get<Vector2>().y * _lookMultiplyer.y, 0, 0));
            if (playerController.playerView.transform.rotation.eulerAngles.x < -80)
            {
                playerController.playerView.transform.rotation.SetEulerAngles(-80, 0, 0);
                Debug.Log("test");
            }
            if (playerController.playerView.transform.rotation.eulerAngles.x > 80)
            {
                playerController.playerView.transform.rotation.SetEulerAngles(80, 0, 0);
            }
        }
    }
}
