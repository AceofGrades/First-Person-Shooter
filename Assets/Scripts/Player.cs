using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = 10f;
    public float jumpHeight = 15f;
    public LayerMask groundLayer;
    public float groundRayDistance = 1.1f;

    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping = false;
    private bool isSprinting = false;

    float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        bool inputJump = Input.GetButtonDown("Jump");

        Vector3 normalized = new Vector3(inputH, 0f, inputV);
        normalized.Normalize();


        Move(normalized.x, normalized.z);

        // If Jump button pressed (Space)
        if (IsGrounded() && inputJump)
        {
            // Make character jump
            Jump(jumpHeight);
        }

        // If NOT Grounded anymore AND is jumping
        if (!IsGrounded() && isJumping)
        {
            isJumping = false;
        }

        // If Is Grounded AND is NOT jumping
        if (IsGrounded() && !isJumping)
        {
            motion.y = 0f;
        }

        motion.y += gravity * Time.deltaTime;

        // Applies motion to CharacterController
        controller.Move(motion * Time.deltaTime);


    }

    // Test if Player is Grounded
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        // Performing raycast
        if(Physics.Raycast(groundRay, groundRayDistance))
        {
            // Return true is hit
            return true; // Exits the function
        }
        //Return false if not hit
        return false; // Exits the function
    }

    // Move the Player Character in direction we give it (horizontal / vertical)
    public void Move(float inputH, float inputV)
    {
        bool inputSprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 direction = new Vector3(inputH, 0f, inputV);
        // Convert local direction to world space
        direction = transform.TransformDirection(direction);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;

        if (IsGrounded() && inputSprint)
        {
            Sprint();
        } else
        {
            speed = walkSpeed;
        }
    }

    // Make the player jump when called
    public void Jump(float height)
    {
        motion.y = jumpHeight;
        isJumping = true;
    }

    public void Sprint()
    {
        speed = runSpeed;
        isSprinting = true;
    }
}
