using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    [Header("Player Specific Scripts")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerLook playerLook;

    [Header("Camera Specific Scripts")]
    [SerializeField] CameraSwitch cameraSwitch;

    [Header("Misc Scripts")]
    [SerializeField] Computer computer;
    [SerializeField] CoffeeRoaster coffeeRoaster;

    PlayerControls controls;
    PlayerControls.PlayerActions player;
    PlayerControls.ComputerActions _computer;
    PlayerControls.CoffeeRoasterActions _coffeeRoaster;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        player = controls.Player;
        _computer = controls.Computer;
        _coffeeRoaster = controls.CoffeeRoaster;

        controls.Computer.Disable();
        controls.CoffeeRoaster.Disable();

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
        player.ExtraInteract.performed += _ => playerInteraction.OnExtraInteract();

        //COMPUTER INTERACTION
        _computer.Exit.performed += _ => computer.OnExit();

        //COFFEE ROASTER INTERACTION
        _coffeeRoaster.Exit.performed += _ => coffeeRoaster.OnExit();
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

    public void EnableDisableControl(string toEnable)
    {
        switch (toEnable)
        {
            case "main":
                controls.Computer.Disable();
                controls.CoffeeRoaster.Disable();
                controls.Player.Enable();
                break;

            case "computer":
                controls.Player.Disable();
                controls.CoffeeRoaster.Disable();
                controls.Computer.Enable();
                break;

            case "coffeeroaster":
                controls.Player.Disable();
                controls.Computer.Disable();
                controls.CoffeeRoaster.Enable();
                break;

            case null:
                Debug.Log("No case");
                break;
        }
    }
}
