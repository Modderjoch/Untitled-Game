using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<CoffeeData> coffeeData = new List<CoffeeData>();
    public List<MachineData> machineData = new List<MachineData>();

    public static GameManager Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene.
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // If no instance exists in the scene, create a new GameObject with the script attached.
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    // Optionally, add any initialization or setup code here.
    private void Awake()
    {
        // Ensure that only one instance of the GameManager exists in the scene.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Add any game-related methods or functionalities here.
    // For example:
    // public void StartGame() { ... }
    // public void PauseGame() { ... }
    // public void EndGame() { ... }
}
