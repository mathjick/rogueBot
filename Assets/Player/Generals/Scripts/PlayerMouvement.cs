using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    public PlayerController playerController;
    [Space(1)]
    [Header("-------------- Mouvement --------------")]
    [Space(1)]
    public Vector3 gravity;
    public Vector3 velocityMax;
    public Vector2 _mouvementMultiplyer;

    public float groundFriction;
    public float groundFrictionWhenImmobile;
    public float airFriction;
    private float frictionModifier = 1;

    [Space(1)]
    [Header("---------------- Jump ----------------")]
    [Space(1)]

    public int jumpForce;
    public int jumps;
    public float bufferBeforeJump;
    public float coyoteTime;
    public Vector3 holdJumpGravity;
    public Vector3 holdJumpVelocityMax;

    public Vector2 playerInput;
    private Vector3 _mouvement;
    private int _jumpsLeft;
    private bool _storedJump;
    private bool _flagTouchGround;
    private int _gravityMode = 0;
    public int velocityMode = 0;

    [Space(1)]
    [Header("---------------- CallBack ----------------")]
    [Space(1)]

    public UnityEvent DoubleJumpCallBack;
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
        if (playerController.isGrounded && _flagTouchGround)
        {
            _jumpsLeft = jumps;
            _flagTouchGround = false;
        }
        if(!playerController.isGrounded && !_flagTouchGround)
        {
            _flagTouchGround = true;
        }
        if (_jumpsLeft > 0 && _storedJump)
        {
            _storedJump = false;
            _jumpsLeft--;
            ExecuteJump();
        }
        _mouvement = playerController.playerTransform.forward * playerInput.y * _mouvementMultiplyer.x + playerController.playerTransform.right * playerInput.x * _mouvementMultiplyer.y;
        var _aimedVelocity = new Vector3(_mouvement.x * Time.deltaTime, playerController.rb.velocity.y, _mouvement.z * Time.deltaTime);
        var trueGravity = gravity;
        if(playerController.actualGround.normal != Vector3.zero && playerController.isGrounded)
        {
            trueGravity = playerController.actualGround.normal * -1;
        }
        playerController.rb.velocity += _gravityMode == 0 ? trueGravity * Time.deltaTime : holdJumpGravity * Time.deltaTime;
        if(velocityMode == 0)
        {
            if(playerInput.x > 0.1 || playerInput.x < -0.1 || playerInput.y > 0.1 || playerInput.y < -0.1)
            {
                playerController.rb.velocity = Vector3.Lerp(playerController.rb.velocity, _aimedVelocity, groundFriction * frictionModifier);
            }
            else
            {
                playerController.rb.velocity = Vector3.Lerp(playerController.rb.velocity, _aimedVelocity, groundFrictionWhenImmobile * frictionModifier);
            }
        }
        else
        {
            playerController.rb.velocity = Vector3.Lerp(playerController.rb.velocity, _aimedVelocity, airFriction * frictionModifier);
        }
        var ClampGround  = new Vector3(Mathf.Clamp(playerController.rb.velocity.x, -velocityMax.x, velocityMax.x), Mathf.Clamp(playerController.rb.velocity.y, -velocityMax.y, velocityMax.y), Mathf.Clamp(playerController.rb.velocity.z, -velocityMax.z, velocityMax.z));
        var ClampAir = new Vector3(Mathf.Clamp(playerController.rb.velocity.x, -holdJumpVelocityMax.x, holdJumpVelocityMax.x), Mathf.Clamp(playerController.rb.velocity.y, -holdJumpVelocityMax.y, holdJumpVelocityMax.y), Mathf.Clamp(playerController.rb.velocity.z, -holdJumpVelocityMax.z, holdJumpVelocityMax.z));
        playerController.rb.velocity = velocityMode == 0 ? ClampGround : ClampAir;
    }

    public void Move(InputValue val)
    {
        if (val.Get<Vector2>().x != 0 || val.Get<Vector2>().y != 0)
        {
            playerInput = val.Get<Vector2>();
        }
        else
        {
            playerInput = Vector3.zero;
        }
    }

    public void Jump(InputValue val)
    {
        if (val.isPressed)
        {
            if (_jumpsLeft > 0)
            {
                if (_jumpsLeft < jumps)
                {
                    DoubleJumpCallBack?.Invoke();
                }
                _jumpsLeft--;
                ExecuteJump();
            }
            else
            {
                this._storedJump = true;
                Invoke("UnstoreJump", bufferBeforeJump);
            }
            _gravityMode = 1;
        }
        else
        {
            _gravityMode = 0;
        }
    }

    public void UnstoreJump()
    {
        this._storedJump = false;
    }

    public void ExecuteJump()
    {
        var gravityCompensator = playerController.rb.velocity.y < 0 ? playerController.rb.velocity.y : 0;
        playerController.rb.AddForce(new Vector3(0, jumpForce -gravityCompensator, 0), ForceMode.Impulse);
    }

    public void modifyFriction(float value)
    {
        frictionModifier = value;
    }

    public void modifyFriction()
    {
        frictionModifier = 1;
    }


    #region LevelUpFallBack
    public void AddAditionalJumps(int value)
    {
        jumps += value;
    }

    public void AddAditionalJumpForce(int value)
    {
        jumpForce += value;
    }

    public void AddAditionalGroundSpeed(float value)
    {
        velocityMax += new Vector3(value, value, value);
    }

    public void AddAditionalAirSpeed(float value)
    {
        holdJumpVelocityMax += new Vector3(value, value, value);
    }

    #endregion
}
