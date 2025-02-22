using UnityEngine;
using System.Collections; // Required for Coroutines

public class SphereCollision : MonoBehaviour
{
    public float bounceForce = 5f; // Adjust bounce strength
    private bool canDeductPoints = true; // Cooldown flag

    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Sphere Collided With: " + collision.collider.name); // Debugging

        if (collision.collider.CompareTag("Terrain") && canDeductPoints)
        {
            // Find the PlayerCollision script and decrease score
            PlayerCollision player = FindObjectOfType<PlayerCollision>();
            if (player != null)
            {
                global::System.Object value = player.DecreaseScore(10);
            }

            // Apply bounce force
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 bounceDirection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
                rb.velocity = bounceDirection.normalized * bounceForce;
            }

            // Start cooldown to prevent instant multiple deductions
            StartCoroutine(ScoreCooldown());
        }
    }

    private IEnumerator ScoreCooldown()
    {
        canDeductPoints = false; // Disable point deduction
        yield return new WaitForSeconds(1f); // Wait for 1 second
        canDeductPoints = true; // Enable point deduction again
    }
}
