using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;

    // Public variables for easy tuning in the Unity Inspector
    public float moveSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f * 2;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();

        //Locking the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Assigning animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            // Reset the vertical velocity 
            playerVelocity.y = -2f;
            animator.SetBool("isJumping", false);
        }

        // Get horizontal and vertical input from the user (WASD or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Handle Animation
        if (moveDirection == Vector3.zero)
        {
            // Idle
            animator.SetFloat("Speed", 0f);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            // Walking
            moveSpeed = 10f;
            animator.SetFloat("Speed", 0.5f);
        }
        else
        {
            // Running
            moveSpeed = 20f;
            animator.SetFloat("Speed", 1.5f);
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
            animator.SetBool("isJumping", true);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

