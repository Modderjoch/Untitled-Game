using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject pcCanvas;
    [SerializeField] Camera camera;

    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private Sprite promptImage;
    [SerializeField] private Sprite promptImage2;
    [SerializeField] private string _name;

    [Header("Screenshot")]
    [SerializeField] private RawImage screen;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public Sprite PromptImage => promptImage;
    public Sprite PromptImage2 => promptImage2;
    public string InteractionName => _name;

    public string screenshotFilename = "computerscreen.png";

    public bool Interact(PlayerInteraction playerInteraction)
    {
        inputManager.SwitchToSystem("computer");

        mainCanvas.SetActive(false);
        pcCanvas.SetActive(true);

        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }

    public void OnExit()
    {
        // Take the screenshot and apply it
        StartCoroutine(TakeAndApplyScreenshotCoroutine());

        StartCoroutine(DeactivateCanvasAfterScreenshot());
    }

    private IEnumerator TakeAndApplyScreenshotCoroutine()
    {
        pcCanvas.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/UI/Images/" + screenshotFilename);

        // Wait until the end of the frame to give time for the screenshot to be captured
        yield return new WaitForSeconds(0.1f);

        // Load the saved screenshot file as a Texture2D
        Texture2D screenshotTexture = LoadScreenshotTexture();

        // Apply the Texture2D to the image
        if (screenshotTexture != null)
        {
            screen.texture = screenshotTexture;
        }
    }

    private Texture2D LoadScreenshotTexture()
    {
        // Read the screenshot file as bytes
        byte[] screenshotBytes = System.IO.File.ReadAllBytes(Application.dataPath + "/UI/Images/" + screenshotFilename);

        // Create a new Texture2D
        Texture2D screenshotTexture = new Texture2D(2, 2); // Provide the initial size of the texture (it will be resized automatically)

        // Load the screenshot bytes into the Texture2D
        if (!screenshotTexture.LoadImage(screenshotBytes))
        {
            Debug.LogError("Failed to load the screenshot as a texture.");
            return null;
        }

        return screenshotTexture;
    }

    private IEnumerator DeactivateCanvasAfterScreenshot()
    {
        // Wait for a slight delay (adjust the duration as needed)
        yield return new WaitForSeconds(0.2f);

        // Deactivate the canvas after the screenshot is captured and applied
        pcCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}