using UnityEngine;

public class SAFirstPersonMonobehavior : MonoBehaviour
{
    public float WalkSpeed = 5.0f;
    public float SprintMultiplier = 2f;
    public float JumpForce = 5.0f;
    public float GroundCheckDistance = 1.5f;
    public float LookSensitivityX = 1f;
    public float LookSensitivityY = 1f;
    public float MinYLookAngle = -90f;
    public float MaxYLookAngle = 90f;
    private Transform playerCamera;
    public float Gravity = -9.81f;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private CharacterController CharacterController;

    

    public Transform PlayerCamera { get => playerCamera; set => playerCamera = value; }

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        moveDirection.Normalize();

        float speed = WalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) // Fix sprinting issue
        {
            speed *= SprintMultiplier;
        }

        CharacterController.Move(moveDirection * speed * Time.deltaTime);

        
        // Fix: Handle Gravity Properly
        if (CharacterController.isGrounded)
        {
          //  Debug.Log("Grounded!"); // Debug message to confirm player is grounded
            velocity.y = -2f; // Keeps player grounded

            // Fix: Apply jump force when pressing Jump
            if (Input.GetButtonDown("Jump")) 
            {
                Debug.Log("Jumping!"); // Debug message to confirm input is detected
                velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);
            }
        }

        // Apply gravity over time
        velocity.y += Gravity * Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);



        
        // Mouse Look
        if (PlayerCamera != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, MinYLookAngle, MaxYLookAngle);

            PlayerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

    }
}
