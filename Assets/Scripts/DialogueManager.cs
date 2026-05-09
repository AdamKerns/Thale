using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public MonoBehaviour playerController;
    private NPCDialogue currentNPC;
    private BadGuyDialogue badNPC;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private string[] currentLines;
    private int currentLineIndex;
    private bool isDialogueActive = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void StartDialogue(string characterName, string[] lines, NPCDialogue npc)
    {
        playerController.enabled = false;
        currentNPC = npc;
        dialoguePanel.SetActive(true);
        nameText.text = characterName;
        currentLines = lines;
        currentLineIndex = 0;
        dialogueText.text = currentLines[currentLineIndex];
        isDialogueActive = true;
    }

    public void StartDialogue(string characterName, string[] lines, BadGuyDialogue npc)
    {
        playerController.enabled = false;
        badNPC = npc;
        dialoguePanel.SetActive(true);
        nameText.text = characterName;
        currentLines = lines;
        currentLineIndex = 0;
        dialogueText.text = currentLines[currentLineIndex];
        isDialogueActive = true;
    }

    void NextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentLines[currentLineIndex];
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        isDialogueActive = false;
        playerController.enabled = true;
        if (currentNPC != null)
        {
            currentNPC.ReturnToSleep();
            currentNPC = null;
        }
        if (badNPC != null)
        {
            badNPC.DialogueFinished();
            badNPC = null;
        }

    }
}