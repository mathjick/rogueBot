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
    public LevelSystem levelSystem;
    public PlayerUnlockable playerUnlockable;
    public bool isGrounded;
    public bool blockInput = false;

    [SerializeField] private IntScriptableEvent updatePlayerHealth;
    [SerializeField] private IntScriptableEvent updatePlayerMaxHealth;


    // Start is called before the first frame update
    void Awake()
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

    private void Start()
    {
        updatePlayerMaxHealth.Trigger(lifeSystem.maxLifePoints);
        updatePlayerHealth.Trigger(lifeSystem.lifePoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        int layer = 17;
        LayerMask ls = 1 << layer;
        isGrounded = Physics.Raycast(playerTransform.position, Vector3.down, 1.1f, ls);
        this.playerMouvementSystem.velocityMode = isGrounded ? 0 : 1;
    }

    public void UpdateHealth()
    {
        updatePlayerHealth.Trigger(lifeSystem.lifePoints);
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

    public void OnReload()
    {
        if (!blockInput)
        {
            playerInventory.Reload();
        }
    }

    public void OnDeath()
    {
        blockInput = true;
        playerUI.SwapUITo(UIType.Death);
    }
}
