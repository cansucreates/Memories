using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance; // Singleton instance

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep playing across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate music
        }
    }
}
