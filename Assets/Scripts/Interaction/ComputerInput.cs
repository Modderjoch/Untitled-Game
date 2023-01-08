using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerInput : MonoBehaviour
{
    public RectTransform resizeWindow;
    [SerializeField] private GameObject toEnableButton;

    private bool maximized = false;
    private Vector2 smallScale;

    public void Maximize()
    {
        smallScale = resizeWindow.sizeDelta;

        resizeWindow.anchoredPosition = new Vector3(0, 37, 0);
        resizeWindow.sizeDelta = new Vector2(1900, 1003);

        maximized = true;

        gameObject.SetActive(false);
        toEnableButton.SetActive(true);
    }

    public void Minimize()
    {
        resizeWindow.anchoredPosition = new Vector3(0, 80, 0);
        resizeWindow.sizeDelta = new Vector2(1300, 665);

        maximized = false;

        Debug.Log("Shrunken Down");

        gameObject.SetActive(false);
        toEnableButton.SetActive(true);
    }
}
