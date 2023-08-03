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

    [Header("Misc Scripts")]
    [SerializeField] Computer computer;

    PlayerControls controls;
    PlayerControls.PlayerActions player;
    PlayerControls.UIActions ui;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    [SerializeField] private bool[] systems;
    [SerializeField] private string[] systemNames;

    private void Awake()
    {
        controls = new PlayerControls();
        player = controls.Player;
        ui = controls.UI;

        ui.Disable();

        //MOVEMENT
        player.Walking.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>(); //Get walking input (WASD)
        player.Jump.performed += _ => playerMovement.OnJump(); //Get jump input
        player.Sprint.performed += _ => playerMovement.OnSprint(); //Get sprint input

        //CAMERA
        player.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>(); //Get x-axis input
        player.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>(); //Get y-axis input
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //INTERACTION
        player.Interact.performed += _ => playerInteraction.OnInteract(); //Get interaction input
        player.ExtraInteract.performed += _ => playerInteraction.OnExtraInteract();

        //USER INTERFACE
        ui.Exit.performed += _ => ExitSystem(CheckActiveSystem());
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

    public void SwitchToSystem(string systemName)
    {
        switch(systemName)
        {
            case "player":
                CursorState("locked");
                systems[0] = true;                
                player.Enable();
                ui.Disable();
                break;
            case "computer":
                systems[0] = false;
                systems[1] = true;
                CursorState("unlocked");
                player.Disable();
                ui.Enable();
                break;
            
        }
    }

    private void ExitSystem(string systemName)
    {
        switch(systemName)
        {
            case "computer":
                systems[1] = false;      
                SwitchToSystem("player");
                computer.OnExit();
                break;
            default: break;
        }
    }

    private string CheckActiveSystem()
    {
        for(int i = 0; i < systems.Length; i++)
        {
            if (systems[i] == true)
            {
                return systemNames[i];
            }
        }
        return null;
    }

    private void CursorState(string state)
    {
        switch (state)
        {
            case "locked":
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case "unlocked":
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            default : break;

        }
    }

    //public void EnableDisableControl(string toEnable)
    //{
    //    switch (toEnable.ToLower())
    //    {
    //        case "main":
    //            controls.Computer.Disable();
    //            controls.CoffeeRoaster.Disable();
    //            controls.Packaging.Disable();
    //            controls.Player.Enable();
    //            break;

    //        case "computer":
    //            controls.Player.Disable();
    //            controls.Computer.Enable();
    //            break;

    //        case "coffeeroaster":
    //            controls.Player.Disable();
    //            controls.CoffeeRoaster.Enable();
    //            break;

    //        case "packaging":
    //            controls.Player.Disable();
    //            controls.Packaging.Enable();
    //            break;

    //        case null:
    //            Debug.Log("No case");
    //            break;
    //    }
    //}
}
