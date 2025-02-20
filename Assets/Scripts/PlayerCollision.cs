using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections; // For the Coroutine

public class PlayerCollision : MonoBehaviour
{
    public float pushForce = 5f; // Strength of the push
    public TextMeshProUGUI scoreText; // TextMeshProUGUI for the score
    private int score = 0; // Player's score
    private CharacterController controller;

    // Cooldown timer variables
    private bool canPushSphere = true; // Flag to control if the player can push the sphere
    public float pushCooldown = 1f; // Cooldown time in seconds

    void Start()
    {
        controller = GetComponent<CharacterController>();
        UpdateScoreUI();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Only allow pushing the sphere if it's not in cooldown
        if (hit.collider.CompareTag("Sphere") && canPushSphere)
        {
            Rigidbody sphereRb = hit.collider.attachedRigidbody;
            if (sphereRb != null)
            {
                // Apply a push force on the sphere
                Vector3 pushDirection = hit.point - transform.position;
                pushDirection.y = 0; // Make sure the push is only horizontal
                pushDirection.Normalize();
                sphereRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                // Increment the score by 10 and update the UI
                score += 10;
                UpdateScoreUI();

                // Start the cooldown before the player can push again
                canPushSphere = false;
                StartCoroutine(CooldownTimer());
            }
        }
    }

    // Detect when the sphere collides with terrain objects (pillars, trees, etc.)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            // Decrease the score by 10 when colliding with terrain
            score -= 10;
            UpdateScoreUI();
        }
    }

    // Coroutine to manage the cooldown time
    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(pushCooldown); // Wait for the cooldown duration
        canPushSphere = true; // Allow the player to push again
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreUI();
    }
}