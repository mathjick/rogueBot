using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera playerView;
    public Transform playerTransform;
    [Space(1)]
    [Header("-------------- Setup Player --------------")]
    [Space(1)]
    public PlayerMouvement playerMouvementSystem;
    public PlayerCamera playerCameraSystem;
    public PlayerAbility playerAbility;
    public PlayerInventory playerInventory;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, 1.1f);
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

    public void OnAbility(InputValue val)
    {
        playerAbility.Activate();
    }

    public void OnPrimaryTrigger(InputValue val)
    {
        playerInventory.HoldTrigger(val);
    }

}
