using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Public variables for easy tuning in the Unity Inspector
    public float moveSpeed = 10f;
    public float jumpHeight = 10f;
    public float gravity = -9.81f * 4;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public Vector3 moveDirection;

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();

        //Locking the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

            // Check if the character is on the ground
            isGrounded = controller.isGrounded;
            if (isGrounded && playerVelocity.y < 0)
            {
                // Reset the vertical velocity 
                playerVelocity.y = -2f;
            }

            // Get horizontal and vertical input from the user (WASD or arrow keys)
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                // Walking
                moveSpeed = 15f;
            }
            else
            {
                // Running
                moveSpeed = 30f;
            }
        
            // Apply movement
            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

                // Rotate the character to face the direction of movement
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // Move the character
                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            }

            // Handle jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

    }
}

