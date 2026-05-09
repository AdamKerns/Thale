using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandsScript : MonoBehaviour
{

    public GameObject interactPrompt;
    public Image blackOut;
    private bool endGame = false;
    public MonoBehaviour CreditsManager;

    void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (endGame && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndCredits());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            endGame = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            endGame = false;
        }
    }

    IEnumerator FadeAndCredits()
    {
        float timer = 0f;

        float duration = 1f;

        Color flashColor = blackOut.color;

        while (timer < duration)

        {

            timer += Time.deltaTime;

            flashColor.a = Mathf.Lerp(0f, 1f, timer / duration);

            blackOut.color = flashColor;

            yield return null;

        }
        interactPrompt.SetActive(false);
        // Display Credits
        CreditsManager.enabled = true;
    }
}
