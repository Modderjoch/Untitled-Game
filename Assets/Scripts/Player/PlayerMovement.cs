using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649

    //Speed and movement based on the horizontal input
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    //Crouching
    [SerializeField] Transform playerCamera;
    bool crouch;

    //Jumping
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    //Simulating gravity
    [SerializeField] float gravity = -30f; // -9.81
    Vector3 verticalVelocity = Vector3.zero;
    

    private void Update()
    {
        GroundedCheck();
    }

    private void Crouch()
    {
        
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

    public void OnCrouch(bool crouchPressed)
    {
        if (crouchPressed)
        {
            crouch = true;
        }
        else if (crouchPressed == false)
        {
            crouch = false;
        }

        Debug.Log("Crouch");
    }
}
