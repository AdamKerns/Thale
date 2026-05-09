using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 200f;

    public Transform playerCamera;

    float xRotation = 0f;

    Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        xRotation = playerCamera.localEulerAngles.x;

        if (xRotation > 180f)
        {
            xRotation -= 360f;
        }
    }

    void Update()
    {
        if (!controller.enabled)
        {
            return;
        }
        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        if (!controller.enabled)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}