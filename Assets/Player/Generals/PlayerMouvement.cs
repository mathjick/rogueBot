using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    public PlayerController playerController;
    private Vector2 playerInput;
    private Vector3 _mouvement;
    public Vector3 gravity;
    public Vector2 _mouvementMultiplyer;
    public int _jumpForce;
    public float reactivityFactor;
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
        _mouvement = playerController.playerTransform.forward * playerInput.y * _mouvementMultiplyer.x + playerController.playerTransform.right * playerInput.x * _mouvementMultiplyer.y;
        var _aimedVelocity = new Vector3(_mouvement.x * Time.deltaTime, playerController.rb.velocity.y, _mouvement.z * Time.deltaTime);
        playerController.rb.velocity = Vector3.Lerp(playerController.rb.velocity, _aimedVelocity, reactivityFactor);
        playerController.rb.velocity += gravity * Time.deltaTime;
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
        playerController.rb.AddForce(new Vector3(0,_jumpForce, 0),ForceMode.Impulse);
    }

}
