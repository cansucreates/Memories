using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTextManager : MonoBehaviour
{
    public TextMeshProUGUI introText; // UI Text
    public float typingSpeed = 0.05f;  // Speed of typing effect
    public float sentenceDelay = 2f;   // Delay before showing the next sentence
    public float fadeDuration = 1f;    // Fade-out duration

    private string[] sentences = new string[]
    {
        "Today is my first day as a young adult. I'm moving into my flat.",
        "I asked my parents to help me around the house. They’ve been with me all day and just left.",
        "It feels empty being by myself for the first time. I guess that’s a part of growing up.",
        "I should tidy up my room, there’s only little to do."
    };

    private int currentSentenceIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        if (introText != null)
        {
            introText.text = ""; // Start with empty text
            StartCoroutine(TypeSentence(sentences[currentSentenceIndex])); // Start typing
        }
    }

void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        if (isTyping)
        {
            // Skip typing effect and show full sentence immediately
            StopAllCoroutines();
            introText.text = sentences[currentSentenceIndex]; 
            isTyping = false;
        }
        else
        {
            // If typing is finished, move to the next sentence
            NextSentence();
        }
    }
}



    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        introText.text = ""; // Clear text

        foreach (char letter in sentence)
        {
            introText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(sentenceDelay); // Wait before showing next sentence

        NextSentence(); // Auto-advance to next sentence
    }

    void NextSentence()
    {
        if (currentSentenceIndex < sentences.Length - 1) // If more sentences remain
        {
            currentSentenceIndex++;
            StartCoroutine(TypeSentence(sentences[currentSentenceIndex])); // Start typing next
        }
        else
        {
            StartCoroutine(FadeOutText()); // If last sentence, fade out text
        }
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color textColor = introText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            introText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }

        introText.gameObject.SetActive(false); // Hide text after fade-out
    }
}
