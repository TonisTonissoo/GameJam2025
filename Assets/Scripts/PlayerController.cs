using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Fall Damage Settings")]
    [SerializeField] private float fallDamageThreshold = -15f;  // Kiirus millest allpool saab damage
    [SerializeField] private float lethalFallThreshold = -25f;   // Kiirus millest allpool sureb

    private Rigidbody2D rb;
    private bool isGrounded;
    private float fallVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horisontaalne liikumine
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Hüppamine
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Salvestame hetke vertikaalkiiruse kukkumise kontrolliks
        fallVelocity = rb.linearVelocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                // 🟢 Esmalt märgime grounded ära
                isGrounded = true;

                // 🔻 Alles nüüd kontrollime kukkumiskiirust
                if (fallVelocity < lethalFallThreshold)
                {
                    Die();
                }
                else if (fallVelocity < fallDamageThreshold)
                {
                    TakeFallDamage();
                }

                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void TakeFallDamage()
    {
        Debug.Log($"Player took fall damage (velocity: {fallVelocity})");
        // Tulevikus lisa HP vähendamine või stun siia
    }

    private void Die()
    {
        Debug.Log($"Player died from fall (velocity: {fallVelocity})");
        // Tulevikus lisa respawn või game over siia
    }
}
