using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{

    public float speed;
    public float runMultiplayer;
    public float gravity = -9.81f;
    public float jumpHeight;
    public float rotationSpeed;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning = false;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float rotateInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Run.performed += ctx => isRunning = true;
        inputActions.Player.Run.canceled += ctx => isRunning = false;
        inputActions.Player.Jump.performed += ctx => jump();
        inputActions.Player.Rotate.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += ctx => rotateInput = 0f;
    }

    void onEnable()
    {
        inputActions.Enable();
    }

    void onDisable()
    {
        inputActions.Disable();
    }



    void jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 move =new Vector3(moveInput.x, 0,moveInput.y);
        move = transform.TransformDirection(move);
        float currentSpeed = isRunning ? speed*runMultiplayer : speed;
        characterController.Move(move * currentSpeed * Time.deltaTime);
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    } 
}
