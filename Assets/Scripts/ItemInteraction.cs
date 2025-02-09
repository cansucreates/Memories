using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInteraction : MonoBehaviour
{
    public float interactionDistance = 10f; // Interaction range
    public TextMeshProUGUI interactionText; // UI text for item description
    public Image itemImage; // UI Image for displaying item
    public GameObject interactionPanel; // UI Panel
    public float typingSpeed = 0.05f; // Speed of typing effect

    private string fullText = ""; // Stores the full text
    private bool isTyping = false; // Tracks if typing is in progress
    private Coroutine typingCoroutine; // Stores the coroutine for stopping

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
                        ShowItemInteraction(item);
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

    void ShowItemInteraction(InteractableItem item)
    {
        // Set image
        if (itemImage != null && item.itemSprite != null)
        {
            itemImage.sprite = item.itemSprite;
        }

        // Show the UI Panel
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
        }

        // Start Typing Effect
        if (interactionText != null)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            fullText = item.interactionText;
            interactionText.text = ""; // Clear previous text
            typingCoroutine = StartCoroutine(TypeText());
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        foreach (char letter in fullText.ToCharArray())
        {
            interactionText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // Use Realtime since Time.timeScale = 0
        }
        isTyping = false;
    }

    void CloseInteraction()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }

        // Resume the game
        Time.timeScale = 1f;
    }

    // Skipping Typing Effect if player clicks while typing
    public void SkipTyping()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            interactionText.text = fullText; // Instantly show full text
            isTyping = false;
        }
    }
}
