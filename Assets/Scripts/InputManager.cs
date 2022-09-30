using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerLook playerLook;

    PlayerControls controls;
    PlayerControls.MovementActions movement;
    PlayerControls.CameraActions camera;
    PlayerControls.InteractionActions interaction;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        movement = controls.Movement;
        camera = controls.Camera;
        interaction = controls.Interaction;

        //MOVEMENT
        movement.Walking.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>(); //Get walking input (WASD)
        movement.Jump.performed += _ => playerMovement.OnJump(); //Get jump input
        //movement.Crouch.performed += _ => playerMovement.OnCrouch(); //Get crouch input
        movement.Sprint.performed += _ => playerMovement.OnSprint(); //Get sprint input

        //CAMERA
        camera.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>(); //Get x-axis input
        camera.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>(); //Get y-axis input
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //INTERACTION
        interaction.Interact.performed += _ => playerInteraction.OnInteract(); //Get interaction input
        interaction.Drop.performed += _ => playerInteraction.OnDrop();
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
