using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [Header("Doors")]
    public GameObject door1;
    public GameObject door2;

    public GameObject newDoor1;
    public GameObject newDoor2;

    [Header("Invisible Wall")]
    public GameObject invisibleWall;


    void Start()
    {
        invisibleWall.SetActive(false);
        newDoor1.SetActive(false);
        newDoor2.SetActive(false);
    }

    public void StartDrawingSequence()
    {
        door1.SetActive(false);
        door2.SetActive(false);
        newDoor1.SetActive(true);
        newDoor2.SetActive(true);
        invisibleWall.SetActive(true);
    }
}