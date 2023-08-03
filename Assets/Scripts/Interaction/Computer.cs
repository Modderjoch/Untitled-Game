using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] Camera camera;
    private Camera mainCam;

    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        mainCanvas.SetActive(false);
        mainCam.GetComponent<CameraSwitcher>().SwitchToCamera(camera);
        inputManager.EnableDisableControl("computer");
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }

    public void OnExit()
    {
        mainCam.GetComponent<CameraSwitcher>().ReturnToMain();

        mainCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}