using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public TMP_Text creditsText;
    public Image thaleImage;

    [TextArea]
    public string[] credits;

    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    void Start()
    {
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        Color textColor = creditsText.color;

        foreach (string credit in credits)
        {
            creditsText.text = credit;

            // Fade In
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                textColor.a = Mathf.Lerp(
                    0f,
                    1f,
                    timer / fadeDuration
                );

                creditsText.color = textColor;

                yield return null;
            }

            yield return new WaitForSeconds(displayDuration);

            // Fade Out
            timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                textColor.a = Mathf.Lerp(
                    1f,
                    0f,
                    timer / fadeDuration
                );

                creditsText.color = textColor;

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        float imageTimer = 0f;

        float duration = 8f;

        Color flashColor = thaleImage.color;

        while (imageTimer < duration)

        {

            imageTimer += Time.deltaTime;

            flashColor.a = Mathf.Lerp(0f, 1f, imageTimer / duration);

            thaleImage.color = flashColor;

            yield return null;

        }

        yield return new WaitForSeconds(2f);

        imageTimer = 0f;

        while (imageTimer < duration)

        {

            imageTimer += Time.deltaTime;

            flashColor.a = Mathf.Lerp(1f, 0f, imageTimer / duration);

            thaleImage.color = flashColor;

            yield return null;

        }
        SceneManager.LoadScene("Main");
    }
    
}