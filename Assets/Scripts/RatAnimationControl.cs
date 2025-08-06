using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatAnimationControl : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Animation
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // Walking
            animator.SetFloat("Speed", 0.5f);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // Running
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}
