using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Speed of rotation
    public Transform playerBody; // Reference to the player's body (for horizontal rotation)

    private float xRotation = 0f; // Tracks vertical rotation (for looking up/down)

    void Start()
    {
        // Hide and lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player left/right (around the Y-axis)
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down (around the X-axis)
        xRotation -= mouseY; // Subtract to invert the vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation to 90 degrees up/down

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}