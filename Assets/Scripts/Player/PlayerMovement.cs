using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649

    //Speed and movement based on the horizontal input
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    float walkSpeed;
    float sprintSpeed;
    float crouchSpeed;
    Vector2 horizontalInput;

    //Crouching
    GameObject playerCamera;
    [SerializeField] Transform playerCapsule;
    bool crouch;

    //Sprinting
    bool sprint;

    //Jumping
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    //Simulating gravity
    [SerializeField] float gravity = -30f; // -9.81
    Vector3 verticalVelocity = Vector3.zero;

    private void Awake()
    {
        walkSpeed = speed;
        sprintSpeed = speed * 3f;
        crouchSpeed = speed * 0.7f;

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        GroundedCheck();
    }

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), 0.1f, groundMask);

        if (isGrounded)
        {
            if (verticalVelocity.y < 0.0f)
            {
                verticalVelocity.y = -2f;
            }
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else

                jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJump()
    {
        jump = true;
    }

    public void OnCrouch()
    {
        crouch = !crouch;

        if (crouch)
        {
            float scale = 0.7f;
            var newScale = playerCapsule.localScale;

            controller.height = 1.4f; //Change the size of the player controller
            newScale.y = scale;
            playerCapsule.localScale = newScale; //Change the scale of the players capsule
            speed = crouchSpeed; //Change the speed to accomodate the slower movement

            var camPos = playerCamera.transform.position; 
            camPos.y = controller.height;
            playerCamera.transform.position = camPos; //Change the camera position to follow the shrink accordingly
        }
        else
        {
            float scale = 1.0f;
            var newScale = playerCapsule.localScale;

            controller.height = 2.0f; //Change the size of the player controller
            newScale.y = scale; 
            playerCapsule.localScale = newScale; //Change the scale of the players capsule
            speed = walkSpeed; //Change the speed to go back to normal movement

            var camPos = playerCamera.transform.position; 
            camPos.y = controller.height - 1f;
            playerCamera.transform.position = camPos; //Change the camera position to follow the shrink accordingly
        }
    }

    public void OnSprint()
    {
        sprint = !sprint;

        if (sprint)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
