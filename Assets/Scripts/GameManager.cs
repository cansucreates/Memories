using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int collectedItems = 0; // Track collected items
    public int totalItems = 5;  // The required number of items

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        collectedItems++; // Increase the count when an item is collected
        Debug.Log("Item Collected! Total: " + collectedItems + "/" + totalItems);
    }

    public bool CanInteractWithBed()
    {
        Debug.Log("Checking if bed interaction is allowed. Collected: " + collectedItems + " / Required: " + totalItems);
        return collectedItems >= totalItems; 
    }
}
