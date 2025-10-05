using UnityEngine;

public class SpikePlatformPiece : MonoBehaviour
{
    private Collider2D solidCollider;
    private Collider2D triggerCollider;

    private void Awake()
    {
        var colliders = GetComponents<Collider2D>();
        foreach (var c in colliders)
        {
            if (c.isTrigger) triggerCollider = c;
            else solidCollider = c;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
