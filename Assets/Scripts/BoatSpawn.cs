using UnityEngine;
using System.Collections;

public class BoatSpawn : MonoBehaviour
{
    public Transform arrivalPoint;
    public Transform boatGroup;
    public Transform playerSpawn;
    public float moveDuration = 3f;
    private CharacterController characterController;
    private playerController playerController;
    private PlayerState playerState;
    private Transform playerTransform;
    public GameObject boatWall;

    void Start()

    {
        boatWall.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
        playerTransform = player.GetComponent<Transform>();

        if (playerState.arrived)
        {
            characterController = player.GetComponent<CharacterController>();
            playerController = player.GetComponent<playerController>();
            player.transform.SetParent(boatGroup);

            StartCoroutine(BoatArrival());

            playerState.arrived = false;

        }
    }

    IEnumerator BoatArrival()

    {

       if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Vector3 startPos = boatGroup.position;
        Quaternion startRot = boatGroup.rotation;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;

            boatGroup.position = Vector3.Lerp(startPos, arrivalPoint.position, t);

            boatGroup.rotation = Quaternion.Lerp(startRot, arrivalPoint.rotation,t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        boatGroup.position = arrivalPoint.position;
        boatGroup.rotation = arrivalPoint.rotation;

        playerTransform.SetParent(null);

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        playerTransform.position = playerSpawn.position;

        if (characterController != null)
        {
            characterController.enabled = true;
        }
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        boatWall.SetActive(true);
    }
}
