using UnityEngine;

public class ItemHighlight : MonoBehaviour
{
    private Renderer itemRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow; // Change this to any highlight color you want

    void Start()
    {
        itemRenderer = GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            originalColor = itemRenderer.material.color; // Store the original color
        }
    }

    void OnMouseEnter()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color = highlightColor; // Change color on hover
        }
    }

    void OnMouseExit()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color = originalColor; // Reset color when not hovering
        }
    }
}
