using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public TextMeshProUGUI gameTitle;
    public Button startButton;
    public IntroTextManager introTextManager; // Reference to IntroTextManager

    void Start()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (startMenu != null)
            startMenu.SetActive(false);

        Time.timeScale = 1f;

        // Instead of just hiding, trigger the intro text
        if (introTextManager != null)
            introTextManager.StartIntroText();

        Debug.Log("Game Started!");
    }
}
