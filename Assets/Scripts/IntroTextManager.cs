using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTextManager : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public GameObject startMenu;
    public float typingSpeed = 0.05f;
    public float sentenceDelay = 2f;
    public float fadeDuration = 1f;

    private string[] sentences = new string[]
    {
        "Today is my first day as a young adult. I'm moving into my flat.",
        "I asked my parents to help me around the house. They’ve been with me all day and just left.",
        "It feels empty being by myself for the first time. I guess that’s a part of growing up.",
        "I should tidy up my room, there’s only little to do."
    };

    private int currentSentenceIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine; // Store the currently running coroutine

    void Start()
    {
        introText.gameObject.SetActive(false);
    }

    public void StartIntroText()
    {
        startMenu.SetActive(false);
        introText.gameObject.SetActive(true);
        introText.text = "";
        currentSentenceIndex = 0;

        // Stop any previous coroutine before starting a new one
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && introText.gameObject.activeSelf)
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                introText.text = sentences[currentSentenceIndex];
                isTyping = false;
            }
            else
            {
                NextSentence();
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        introText.text = "";

        foreach (char letter in sentence)
        {
            introText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(sentenceDelay);
        NextSentence();
    }

    void NextSentence()
    {
        if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;

            // Stop any existing coroutine before starting a new one
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
        }
        else
        {
            StartCoroutine(FadeOutText());
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

        introText.gameObject.SetActive(false);
    }
}
