using UnityEngine;
using TMPro; // For UI Text

public class PointsCounter : MonoBehaviour
{
    public int points = 0; // Player's score
    public TextMeshProUGUI pointsText; // UI text reference
    public float pushForce = 5f; // Force to roll the ball

    void Start()
    {
        UpdatePointsText(); // Ensure the UI starts at 0
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Ball")) // Ensure the ball has the correct tag
        {
            points += 10; // Increase points by 10
            UpdatePointsText(); // Update UI

            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null && !rb.isKinematic) // Check if ball has Rigidbody
            {
                Vector3 pushDirection = hit.gameObject.transform.position - transform.position;
                pushDirection.y = 0; // Keep movement horizontal
                pushDirection.Normalize();
                
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse); // Apply rolling force
            }
        }
    }

    void UpdatePointsText()
    {
        pointsText.text = "Points: " + points; // Update UI text
    }
}
