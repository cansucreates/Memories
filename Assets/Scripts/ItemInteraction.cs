using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ItemInteraction : MonoBehaviour
{
    public float interactionDistance = 10f; // Interaction range
    public TextMeshProUGUI interactionText; // UI text for item description
    public Image itemImage; // UI Image for displaying item
    public GameObject interactionPanel; // UI Panel

    void Start()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); // UI is hidden at the start
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    InteractableItem item = hit.collider.GetComponent<InteractableItem>();

                    if (item != null)
                    {
                        // Set text
                        if (interactionText != null)
                        {
                            interactionText.text = item.interactionText;
                        }

                        // Set image
                        if (itemImage != null && item.itemSprite != null)
                        {
                            itemImage.sprite = item.itemSprite; // Assign the sprite
                        }

                        // Show the UI Panel
                        if (interactionPanel != null)
                        {
                            interactionPanel.SetActive(true);
                        }

                        // Pause the game
                        Time.timeScale = 0f;
                    }
                }
            }
        }

        // Close UI with Escape key
        if (Input.GetKeyDown(KeyCode.Escape) && interactionPanel.activeSelf)
        {
            CloseInteraction();
        }
    }

    public void CloseInteraction()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }

        // Resume the game
        Time.timeScale = 1f;
    }
}
