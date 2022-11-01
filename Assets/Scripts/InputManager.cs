using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    //Player specific input
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerLook playerLook;

    //Camera specific input
    [SerializeField] CameraSwitch cameraSwitch;

    //Computer input
    [SerializeField] Computer computer;

    PlayerControls controls;
    PlayerControls.PlayerActions player;
    PlayerControls.ComputerActions computerA;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        player = controls.Player;
        computerA = controls.Computer;

        controls.Computer.Disable();

        //MOVEMENT
        player.Walking.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>(); //Get walking input (WASD)
        player.Jump.performed += _ => playerMovement.OnJump(); //Get jump input
        player.Sprint.performed += _ => playerMovement.OnSprint(); //Get sprint input

        //CAMERA
        player.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>(); //Get x-axis input
        player.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>(); //Get y-axis input
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.Switch.performed += ctx => cameraSwitch.Switch(); //switch cameras

        //INTERACTION
        player.Interact.performed += _ => playerInteraction.OnInteract(); //Get interaction input

        //COMPUTER INTERACTION
        computerA.Exit.performed += _ => computer.OnExit();
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

    public void OnComputer()
    {
        controls.Player.Disable();
        controls.Computer.Enable();
    }

    public void OffComputer()
    {
        controls.Player.Enable();
        controls.Computer.Disable();
    }

    public void EnableDisableControl(string toEnable)
    {
        switch (toEnable)
        {
            case "main":
                controls.Computer.Disable();
                controls.Player.Enable();
                break;

            case "computer":
                controls.Player.Disable();
                controls.Computer.Enable();
                break;

            case null:
                Debug.Log("No case");
                break;
        }
    }
}
