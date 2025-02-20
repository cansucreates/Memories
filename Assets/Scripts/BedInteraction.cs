using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    public EndGameManager endGameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject) // Check if it's the bed
                {
                    if (GameManager.Instance.CanInteractWithBed()) 
                    {
                        Debug.Log("✅ All items collected! Showing end game screen...");
                        endGameManager.ShowEndGameScreen();
                    }
                    else
                    {
                        Debug.Log("❌ You need to collect all items before sleeping!");
                    }
                }
            }
        }
    }
}
