using System.Collections;
using UnityEngine;

public class CrackedPlatformPiece : MonoBehaviour
{
    [SerializeField] private float breakDelay = 0.5f;

    private Collider2D solidCollider;
    private Collider2D triggerCollider;
    private SpriteRenderer sr;

    private void Awake()
    {

        var colliders = GetComponents<Collider2D>();
        foreach (var c in colliders)
        {
            if (c.isTrigger) triggerCollider = c;
            else solidCollider = c;
        }

        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BreakAfterDelay());
        }
    }

    private IEnumerator BreakAfterDelay()
    {
        yield return new WaitForSeconds(breakDelay);

        solidCollider.enabled = false;
        sr.enabled = false;

    }
}
