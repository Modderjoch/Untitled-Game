using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject computerCamera;

    private bool fpsIsOn = true;
    public bool compIsOn = false;

    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void Switch()
    {
        if (fpsIsOn)
        {
            firstPersonCamera.SetActive(false);
            computerCamera.SetActive(true);

            fpsIsOn = false;
            compIsOn = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else if (compIsOn)
        {
            firstPersonCamera.SetActive(true);
            computerCamera.SetActive(false);

            fpsIsOn = true;
            compIsOn = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
