using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI pickupPrompt;
    public GameObject interactionPanel;

    private InteractableItem currentItem;
    private bool canPickUp = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);
    }

 void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                InteractableItem item = hit.collider.GetComponent<InteractableItem>();
                if (item != null)
                    ShowItemInteraction(item);
            }
        }
    }

    if (canPickUp && Input.GetKeyDown(KeyCode.E))
        PickUpItem();

    if (Input.GetKeyDown(KeyCode.Escape) && interactionPanel.activeSelf)
        CloseInteraction();
}


    void ShowItemInteraction(InteractableItem item)
    {
        currentItem = item;

        if (interactionPanel != null)
            interactionPanel.SetActive(true);

        if (interactionText != null)
            interactionText.text = item.interactionText;

        if (pickupPrompt != null)
        {
            pickupPrompt.text = "Press E to pick up " + currentItem.itemName;
            pickupPrompt.gameObject.SetActive(true);
            canPickUp = true;
        }

        Time.timeScale = 0f;
    }

    void PickUpItem()
    {
        if (currentItem != null)
        {
            currentItem.gameObject.SetActive(false);
            GameManager.Instance.CollectItem(); // Notify GameManager when an item is picked up
        }

        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        Time.timeScale = 1f;
        currentItem = null;
        canPickUp = false;
    }

    void CloseInteraction()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        Time.timeScale = 1f;
        currentItem = null;
        canPickUp = false;
    }


}
