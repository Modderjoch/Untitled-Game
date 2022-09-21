using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerLook playerLook;

    PlayerControls controls;
    PlayerControls.MovementActions movement;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        movement = controls.Movement;

        //Get Movement Input (WASD)
        movement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        //Get Spacebar Input
        movement.Jump.performed += _ => playerMovement.OnJump();

        //Get Mouse Input
        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        playerMovement.ReceiveInput(horizontalInput);
        playerLook.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
