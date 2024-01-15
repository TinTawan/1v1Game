using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;


    InputManager inputManager;

    private Transform camTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();


    }


    void PlayerMove()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0f;
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);

        //rotate player same y as cam is rotating
        transform.rotation = Quaternion.Euler(0f, camTransform.eulerAngles.y, 0f);
    }
    
    void PlayerJump()
    {
        //jump
        if (inputManager.GetPlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        //ground check
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -.1f;
        }
    }
}
