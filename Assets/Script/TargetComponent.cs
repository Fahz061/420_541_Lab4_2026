
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    private Renderer targetRenderer;
    private Color originalColor;

    public Color hitColor = Color.green;

    private void Start()
    {
        targetRenderer = GetComponentInChildren<Renderer>();

        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameManager.Instance.IncrementScore();

            // Change the target color.
            if (targetRenderer != null)
            {
                targetRenderer.material.color = hitColor;
            }

            // Restore the original color after 5 seconds.
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 5f);
        }
    }

    private void ResetColor()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = originalColor;
        }
    }
}