using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] CameraSwitch cameraSwitch;
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject mainCanvas;

    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        mainCanvas.SetActive(false);
        cameraSwitch.Switch();
        inputManager.EnableDisableControl("computer");
        return true;
    }

    public void OnExit()
    {
        if (cameraSwitch.compIsOn)
        {
            mainCanvas.SetActive(true);
            cameraSwitch.Switch();
            inputManager.EnableDisableControl("main");
        }        
    }
}
