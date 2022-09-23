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

    bool controllerConnected = false;

    private void Awake()
    {
        controls = new PlayerControls();
        movement = controls.Movement;

        //Get Movement Input (WASD)
        movement.Walking.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        //Get Jump Input
        movement.Jump.performed += _ => playerMovement.OnJump();

        //Get Crouch Input
        movement.Crouch.performed += _ => playerMovement.OnCrouch();

        //Get Sprint Input
        movement.Sprint.performed += _ => playerMovement.OnSprint();

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
