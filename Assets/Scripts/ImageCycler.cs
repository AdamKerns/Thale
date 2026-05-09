using UnityEngine;
using UnityEngine.UI;

public class ImageCycler : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject imageCanvas;

    [Header("Art")]
    public Sprite[] artChoices;
    public Image artDisplay;

    [Header("References")]
    public BadGuyDialogue badGuy;
    private int currentArt = 0;
    private bool canControlArt = false;

    void Start()
    {
        imageCanvas.SetActive(false);

        if (artChoices.Length > 0)
        {
            artDisplay.sprite = artChoices[currentArt];
        }
    }

    void Update()
    {
        if (!canControlArt) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextArt();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousArt();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowAway();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FinishArt();
        }
    }

   public void OpenDrawingUI()
    {
        imageCanvas.SetActive(true);
        canControlArt = true;
        if (badGuy != null)
        {
            badGuy.StartNaggingDialogue();
        }

    }

    public void CloseDrawingUI()
    {
        imageCanvas.SetActive(false);
        canControlArt = false;
    }

    public void NextArt()
    {
        if (artChoices.Length == 0) return;

        currentArt++;

        if (currentArt >= artChoices.Length)
        {
            currentArt = 0;
        }

        artDisplay.sprite = artChoices[currentArt];
    }

    public void PreviousArt()
    {
        if (artChoices.Length == 0) return;

        currentArt--;

        if (currentArt < 0)
        {
            currentArt = artChoices.Length - 1;
        }

        artDisplay.sprite = artChoices[currentArt];
    }

    public void ThrowAway()
    {
        Debug.Log("Art thrown away");

        currentArt = 0;
        artDisplay.sprite = artChoices[currentArt];

        if (badGuy != null)
        {
            badGuy.StartNaggingDialogue();
        }
    }

    public void FinishArt()
    {
        Debug.Log("Art completed");

        CloseDrawingUI();

        if (badGuy != null)
        {
            badGuy.StartDyingDialogue();
        }
    }

    public Sprite GetCurrentArt()
    {
        return artChoices[currentArt];
    }
}