using UnityEngine;
using System.Collections;

public class CubeGrowth : MonoBehaviour
{
    public float scaleMultiplier = 1.5f; // Growth factor per collision
    public float scaleDuration = 3f; // Time to scale up
    public float cooldownDuration = 3f; // Cooldown before triggering again

    private bool canGrow = true; // Cooldown flag

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canGrow)
        {
            StartCoroutine(GrowCube());
        }
    }

    private IEnumerator GrowCube()
    {
        canGrow = false;

        Vector3 currentScale = transform.localScale; // Get current scale
        Vector3 targetScale = currentScale * scaleMultiplier; // New target scale

        float initialHeight = currentScale.y; 
        float targetHeight = targetScale.y;
        float heightDifference = (targetHeight - initialHeight); // Full height difference

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y + (heightDifference / 2f), startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            float progress = elapsedTime / scaleDuration;
            transform.localScale = Vector3.Lerp(currentScale, targetScale, progress);
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set exactly
        transform.localScale = targetScale;
        transform.position = new Vector3(startPosition.x, startPosition.y + (heightDifference / 2f), startPosition.z);

        yield return new WaitForSeconds(cooldownDuration);
        canGrow = true;
    }
}
