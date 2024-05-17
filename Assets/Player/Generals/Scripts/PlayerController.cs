using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public LifeSystem lifeSystem;
    public bool isGrounded;
    public bool blockInput = false;

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
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, 1.1f,9);
        this.playerMouvementSystem.velocityMode = isGrounded ? 0 : 1;
    }

    public void OnMove(InputValue val)
    {
        if (!blockInput)
        {
            playerMouvementSystem.Move(val);
        }
    }

    public void OnLook(InputValue val)
    {
        if (!blockInput)
        {
            playerCameraSystem.Look(val);
        }
    }

    public void OnJump(InputValue val)
    {
        if (!blockInput)
        {
            playerMouvementSystem.Jump(val);
        }
    }

    public void OnAbility(InputValue val)
    {
        if (!blockInput)
        {
            playerAbility.Activate();
        }
    }

    public void OnPrimaryTrigger(InputValue val)
    {
        if (!blockInput)
        {
            playerInventory.HoldTrigger(val);
        }
    }

    public void OnInteract()
    {
        if (blockInput)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnDeath()
    {
        blockInput = true;
        playerUI.SwapUITo(UIType.Death);
    }
}
