using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Boat : MonoBehaviour
{
    public GameObject interactPrompt;

    private PlayerState playerState;
    private bool playerNearby = false;

    public Transform playerRoot;

    public Transform playerCamera;

    public Transform boatGroup;

    public MonoBehaviour playerController;

    public float sailSpeed;
    public float sailDuration;
    private bool sailing = false;
    public Transform lookTarget;
    public CharacterController characterController;
    public GameObject boatWall;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !sailing && playerState != null && playerState.hasOar)
        {
            Destroy(boatWall);
            sailing = true;

            playerController.enabled = false;
            characterController.enabled = false;

            playerRoot.SetParent(boatGroup);

            playerCamera.position = transform.position + new Vector3(0, 0.5f, 0);
            
            Vector3 direction = lookTarget.position - playerRoot.position;

            direction.y = 0f;

            playerRoot.rotation = Quaternion.LookRotation(direction);

            playerCamera.localRotation = Quaternion.identity;

            StartCoroutine(Sail());
        }
    }

    IEnumerator Sail()
    {
        float timer = 0f;

        while (timer < sailDuration)
        {
            boatGroup.Translate(Vector3.left * sailSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            yield return null;
        }
        playerState.arrived = true;
        SceneManager.LoadScene("BGI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerState = other.GetComponent<PlayerState>();

            playerNearby = true;

            if (interactPrompt != null &&
                playerState != null &&
                playerState.hasOar)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }
}