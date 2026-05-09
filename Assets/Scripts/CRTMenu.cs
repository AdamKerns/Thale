using UnityEngine;
using System.Collections;
public class CRTMenu : MonoBehaviour
{
    public Animator animator;
    public MonoBehaviour playerController;
    public Camera camera;
    private bool yesSelected = true; 
    private bool menuLocked = false;
    public AudioSource audioSource;
    public AudioClip menuLoop;
    public AudioClip selectClip;

    void Start()
    {
        audioSource.clip = menuLoop;
        audioSource.loop = true;
        audioSource.Play();
        animator.SetBool("YesSelected", true);
    }

    void Update()
    {
        if (menuLocked) return;

        playerController.enabled = false;

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            yesSelected = !yesSelected;
            animator.SetBool("YesSelected", yesSelected);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuLocked = true;
            if (yesSelected)
            {
                StartCoroutine(FOVTransition(40f, 60f, 3f));
                animator.SetTrigger("StartGame");
                StartCoroutine(FinishLoopThenPlay());
                StartCoroutine(StartGameSequence());
            }
            else
            {
                animator.SetTrigger("QuitGame");
                StartCoroutine(QuitSequence());
            }
        }


    }

    IEnumerator FinishLoopThenPlay()
    {
        audioSource.loop = false;
        yield return new WaitWhile(() => audioSource.isPlaying);
        audioSource.clip = selectClip;
        audioSource.Play();
    }

    IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Gameplay Starts");
        playerController.enabled = true;
    }

    IEnumerator QuitSequence()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
        Debug.Log("Quit Game");
    }

    IEnumerator FOVTransition(float start, float end, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            camera.fieldOfView = Mathf.Lerp(start, end, elapsed / time);
            yield return null;
        }
        camera.fieldOfView = end;
    }
}