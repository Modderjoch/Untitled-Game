using UnityEngine;

public class CameraMorphController : MonoBehaviour
{
    public Transform presetCameraPosition;
    public Quaternion presetCameraRotation;
    public float morphSpeed = 3.0f;

    private bool isMorphing = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isMorphing)
        {
            // Morph the camera towards the preset position
            transform.position = Vector3.Lerp(transform.position, presetCameraPosition.position, Time.deltaTime * morphSpeed);

            // Apply the inverted rotation to the camera
            //transform.rotation = Quaternion.Slerp(transform.rotation, presetCameraRotation, Time.deltaTime * morphSpeed);

            // Optionally, you can add a condition to return to the first-person view after a certain time or when another condition is met.
        }
    }

    public void StartMorph(Transform positionToMorph, Quaternion rotationToMorph)
    {
        presetCameraPosition = positionToMorph;
        presetCameraRotation = rotationToMorph;
        isMorphing = true;
    }

    // Function to return the camera to the first-person view
    public void ReturnToFirstPersonView()
    {
        isMorphing = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
