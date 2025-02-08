using TMPro;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public float interactionDistance = 10f; // How far the player can interact
    public TextMeshProUGUI interactionText; // Reference to the TextMeshPro UI text element

    void Start()
    {
        // Hide the text initially
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Check if the object is interactable
                if (hit.collider.CompareTag("Interactable"))
                {
                    // Get the InteractableItem component
                    InteractableItem item = hit.collider.GetComponent<InteractableItem>();

                    if (item != null)
                    {
                        // Display the text from the InteractableItem component
                        if (interactionText != null)
                        {
                            interactionText.text = item.interactionText;
                            interactionText.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
