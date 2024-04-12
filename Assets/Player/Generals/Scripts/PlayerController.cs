using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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
    public PlayerUI playerUI;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, 1.1f);
        this.playerMouvementSystem.velocityMode = isGrounded ? 0 : 1;
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
