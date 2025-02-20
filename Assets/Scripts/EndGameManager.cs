using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public GameObject endGamePanel;

    void Start()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(false); // Hide the panel at the start
    }

    public void ShowEndGameScreen()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true); // Show the panel
            Time.timeScale = 0f; // Pause game
        }
    }
}
