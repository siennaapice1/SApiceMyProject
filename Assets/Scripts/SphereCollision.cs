using UnityEngine;
using TMPro; // Required for TextMeshPro

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class SphereCollision : MonoBehaviour
{
    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Sphere Collided With: " + collision.collider.name); // Debugging

        if (collision.collider.CompareTag("Terrain"))
        {
            // Find the PlayerCollision script and decrease score
            PlayerCollision player = FindObjectOfType<PlayerCollision>();
            if (player != null)
            {
                player.DecreaseScore(10); // Call a method to decrease score
            }
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}