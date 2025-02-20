using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushForce = 5f; // Adjust push strength


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        // Ensure the object has a Rigidbody and isn't kinematic
        if (rb != null && !rb.isKinematic)
        {
            // Calculate push direction (horizontal only)
            Vector3 pushDirection = hit.gameObject.transform.position - transform.position;
            pushDirection.y = 0; // Keep push horizontal
            pushDirection.Normalize();

            // Apply force
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
