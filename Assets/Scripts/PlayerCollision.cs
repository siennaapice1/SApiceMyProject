

using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;
using System; // For the Coroutine

public class PlayerCollision : MonoBehaviour
{
    public float pushForce = 5f; // Strength of the push
    public TextMeshProUGUI scoreText; // TextMeshProUGUI for the score
    protected int score = 0; // Player's score
    private CharacterController controller;

    // Cooldown timer variables
    private bool canPushSphere = true; // Flag to control if the player can push the sphere
    public float pushCooldown = 1f; // Cooldown time in seconds

    // Audio Source and Sound Effect
    public AudioClip collisionSound; // Sound to play on collision
    private AudioSource audioSource; // The audio source component

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>(); // Get or add the AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Ensure an AudioSource exists
        }
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

                // Increment the score by 25 and update the UI
                score += 25;
                UpdateScoreUI();

                // Play the collision sound if set
                if (collisionSound != null)
                {
                    audioSource.PlayOneShot(collisionSound); // Play the sound once
                }

                // Start the cooldown before the player can push again
                canPushSphere = false;
                StartCoroutine(CooldownTimer());
            }
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

    internal System.Object DecreaseScore(int v)
    {
        score -= v;
        UpdateScoreUI();
        return score;
    }
}
