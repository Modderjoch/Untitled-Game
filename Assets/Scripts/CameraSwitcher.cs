using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1; // Reference to the first camera
    public float transitionDuration = 2f; // Duration of the transition in seconds
    public bool useSmoothTransition = true; // Use smooth movement instead of linear
    private Transform targetCameraTransform; // The target camera's transform
    private bool transitioning = false; // Flag to check if a transition is in progress
    private Transform originTransform;
    private Camera currentCamera;
    private Camera originCamera;
    private InputManager inputManager;

    private void Awake()
    {
        originTransform = transform;
        originCamera = Camera.main;
        inputManager = GameObject.Find("Player").GetComponent<InputManager>();
    }

    // Other methods and functionality can be implemented here
    public void SwitchToCamera(Camera camera)
    {
        camera.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerInteraction>().disableInteract = true;

        if (transitioning) return; // Avoid overlapping transitions
        transitioning = true;

        // Set the target camera to the second camera
        targetCameraTransform = camera.transform;

        // Start the coroutine for the camera movement
        StartCoroutine(MoveCamera(camera));
        gameObject.SetActive(false);

        currentCamera = camera;
    }

    private IEnumerator MoveCamera(Camera camera)
    {
        Transform currentCameraTransform = Camera.main.transform; // Assuming the current camera is the main camera
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            if (useSmoothTransition)
            {
                // Use smooth movement using Slerp for rotation and Lerp for position
                currentCameraTransform.rotation = Quaternion.Slerp(currentCameraTransform.rotation, targetCameraTransform.rotation, t);
                currentCameraTransform.position = Vector3.Lerp(currentCameraTransform.position, targetCameraTransform.position, t);
            }
            else
            {
                // Use linear movement
                currentCameraTransform.rotation = Quaternion.Lerp(currentCameraTransform.rotation, targetCameraTransform.rotation, t);
                currentCameraTransform.position = Vector3.Lerp(currentCameraTransform.position, targetCameraTransform.position, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera is exactly in the target position and rotation
        currentCameraTransform.rotation = targetCameraTransform.rotation;
        currentCameraTransform.position = targetCameraTransform.position;

        transitioning = false;
    }

    public void ReturnToMain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentCamera.gameObject.SetActive(false);
        gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerInteraction>().disableInteract = false;

        if (transitioning) return; // Avoid overlapping transitions
        transitioning = true;

        // Set the target camera to the second camera
        targetCameraTransform = originTransform;

        // Start the coroutine for the camera movement
        StartCoroutine(MoveCamera(originCamera));

        inputManager.EnableDisableControl("Main");
    }
}

