using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] CameraSwitch cameraSwitch;
    [SerializeField] InputManager inputManager;

    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Opening PC");
        cameraSwitch.Switch();
        inputManager.EnableDisableControl("computer");
        return true;
    }

    public void OnExit()
    {
        Debug.Log("Exit PC");

        if (cameraSwitch.compIsOn)
        {
            cameraSwitch.Switch();
            inputManager.EnableDisableControl("main");
        }        
    }
}
