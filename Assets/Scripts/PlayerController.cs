using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintSpeed = 15.0f;
    public float rotationSpeed = 120.0f;
    public float jumpForce = 5.0f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    public float maxLookAngle = 90f;

    private float verticalLookRotation = 0f;
    private bool jumpRequested = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        bool moveForward = Input.GetKey(KeyCode.W);
        bool moveBackward = Input.GetKey(KeyCode.S);
        bool moveLeft = Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.D);
        bool sprinting = Input.GetKey(KeyCode.LeftShift);

        if (moveForward)
        {
            Vector3 forwardMovement = transform.forward * (sprinting ? sprintSpeed : speed) * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }

        if (moveBackward)
        {
            Vector3 backwardMovement = -transform.forward * (sprinting ? sprintSpeed : speed) * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + backwardMovement);
        }

        if (moveLeft)
        {
            Vector3 leftMovement = -transform.right * (sprinting ? sprintSpeed : speed) * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + leftMovement);
        }

        if (moveRight)
        {
            Vector3 rightMovement = transform.right * (sprinting ? sprintSpeed : speed) * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + rightMovement);
        }

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpRequested = false;
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }
}