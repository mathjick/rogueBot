using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public PlayerController playerController;
    [Space(1)]
    [Header("------------ Look Parameters ------------")]
    [Space(1)]
    public Vector2 _lookMultiplyer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(InputValue val)
    {
        if(Mathf.Abs(val.Get<Vector2>().x) <= 1 && Mathf.Abs(val.Get<Vector2>().y) <= 1)
        {
        }
        else
        {
            playerController.playerTransform.Rotate(new Vector3(0, val.Get<Vector2>().x * _lookMultiplyer.x, 0));
            playerController.playerView.transform.Rotate(new Vector3(-val.Get<Vector2>().y * _lookMultiplyer.y, 0, 0));
            playerController.playerView.transform.localRotation = Quaternion.Euler(playerController.playerView.transform.localRotation.eulerAngles.x, 0, 0);
            if (playerController.playerView.transform.rotation.eulerAngles.x > 180 && playerController.playerView.transform.rotation.eulerAngles.x < 280)
            {
                playerController.playerView.transform.rotation = Quaternion.Euler(-80, playerController.playerView.transform.rotation.eulerAngles.y, playerController.playerView.transform.rotation.eulerAngles.z);
            }
            if (playerController.playerView.transform.rotation.eulerAngles.x > 80 && playerController.playerView.transform.rotation.eulerAngles.x < 180)
            {
                playerController.playerView.transform.rotation = Quaternion.Euler(80, playerController.playerView.transform.rotation.eulerAngles.y, playerController.playerView.transform.rotation.eulerAngles.z);
            }
        }
    }
}
