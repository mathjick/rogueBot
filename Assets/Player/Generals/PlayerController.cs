using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera playerView;
    public Transform playerTransform;
    public PlayerMouvement playerMouvementSystem;
    public PlayerCamera playerCameraSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue val)
    {
        playerMouvementSystem.Move(val);
    }

    public void OnLook(InputValue val)
    {
        playerCameraSystem.Look(val);
    }

    public void OnJump(InputValue val)
    {
        playerMouvementSystem.Jump(val);
    }

}
