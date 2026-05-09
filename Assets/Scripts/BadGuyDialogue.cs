using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BadGuyDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public CharacterController player;
    public ImageCycler imageCycler;
    public GameObjectManager gameObjectManager;
    [TextArea] public string[] initialLines;
    [TextArea] public string[] naggingLines;
    [TextArea] public string[] dyingLines;

    public string characterName = "Vinny";

    private bool playerInRange = false;
    public Image whiteFlash;
    public GameObject house;
    public GameObject badGuyObject;
    public GameObject hands;

    public enum DialoguePhase
    {
        None,
        Initial,
        Nagging,
        Dying
    }

    public DialoguePhase currentPhase = DialoguePhase.None;

    private bool introStarted = false;
    public GameObject interactPrompt;

    void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && !introStarted)
        {
            introStarted = true;

            currentPhase = DialoguePhase.Initial;

            dialogueManager.StartDialogue(
                characterName,
                initialLines,
                this
            );
        }
    }

    public void StartNaggingDialogue()
    {
        currentPhase = DialoguePhase.Nagging;

        dialogueManager.StartDialogue(
            characterName,
            naggingLines,
            this
        );
    }

    public void StartDyingDialogue()
    {
        currentPhase = DialoguePhase.Dying;

        dialogueManager.StartDialogue(
            characterName,
            dyingLines,
            this
        );
    }

    // Called from DialogueManager
    public void DialogueFinished()
    {
        switch (currentPhase)
        {
            case DialoguePhase.Initial:
                Debug.Log("Start drawing phase");
                imageCycler.OpenDrawingUI();
                gameObjectManager.StartDrawingSequence();
                break;

            case DialoguePhase.Nagging:
                Debug.Log("Nagging finished");
                break;

            case DialoguePhase.Dying:
                Debug.Log("Bad guy defeated");
                StartCoroutine(DeathSequence());
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator DeathSequence()
    {
        float timer = 0f;

        float duration = 1f;

        Color flashColor = whiteFlash.color;

        // Fade to white

        while (timer < duration)

        {

            timer += Time.deltaTime;

            flashColor.a = Mathf.Lerp(0f, 1f, timer / duration);

            whiteFlash.color = flashColor;

            yield return null;

        }

        // Remove house and bad guy

        house.SetActive(false);

        Renderer[] renderers = badGuyObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        Collider[] colliders = badGuyObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)

        {
            c.enabled = false;
        }

        yield return new WaitForSeconds(0.5f);

        // Fade back out

        timer = 0f;

        while (timer < duration)

        {

            timer += Time.deltaTime;

            flashColor.a = Mathf.Lerp(1f, 0f, timer / duration);

            whiteFlash.color = flashColor;

            yield return null;

        }

        hands.SetActive(true);
    }
}