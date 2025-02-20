using UnityEngine;

public class LockYUnlessJumping : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        // Prevent player from sinking below Y = 0
        if (isGrounded && transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, moveDirection.y, moveZ) * moveSpeed;

        // Apply jump force if grounded
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
        else if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; // Apply gravity when airborne
        }

        controller.Move(moveDirection * Time.deltaTime);
    }
}
