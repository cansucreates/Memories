using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 50f; // Speed of rotation
    public Transform playerBody; // Reference to the player's body (for horizontal rotation)

    private float xRotation = 0f; // Tracks vertical rotation (for looking up/down)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // Keep the cursor inside the game window
        Cursor.visible = true; // Keep cursor visible
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is held
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get raw mouse input for more accurate movement
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player left/right (Y-axis)
            playerBody.Rotate(Vector3.up * mouseX);

            // Rotate the camera up/down (X-axis)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock cursor when not right-clicking
            Cursor.visible = true;
        }
    }
}
