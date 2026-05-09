using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject interactPrompt;
    public Transform awakeTeleport;
    public Transform asleepTeleport;
    public Transform playerTeleport;
    public CharacterController player;
    public Animator animator;
    public GameObject OarParent;

    [TextArea]
    public string[] dialogueLines;

    public string characterName = "Thale";

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(characterName, dialogueLines, this);
            transform.position = awakeTeleport.position;
            transform.rotation = awakeTeleport.rotation;
            player.enabled = false;
            player.transform.position = playerTeleport.position;
            player.enabled = true;
            interactPrompt.SetActive(false);
            playerInRange = false;
            animator.SetBool("WakeUp", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.SetActive(false);
        }
    }

    public void ReturnToSleep()
    {
        transform.position = asleepTeleport.position;
        transform.rotation = asleepTeleport.rotation;
        animator.SetBool("WakeUp", false);
        OarParent.SetActive(true);
    }
}