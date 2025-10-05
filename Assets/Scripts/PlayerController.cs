using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Fall Damage Settings")]
    [SerializeField] private float fallDamageThreshold = -15f;  // Kiirus millest allpool saab damage
    [SerializeField] private float lethalFallThreshold = -25f;   // Kiirus millest allpool sureb

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;   // mitu elu on maksimaalselt
    private int currentHealth;

    [Header("Respawn Settings")]
    [SerializeField] private Transform respawnPoint;


    private Rigidbody2D rb;
    private bool isGrounded;
    private float fallVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void TakeFallDamage()
    {
        Debug.Log($"Player took fall damage");
        TakeDamage(1);
    }

    private void Die()
    {
        Debug.Log($"Player died)");
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        Camera.main.transform.position = new Vector3(
        respawnPoint.position.x,
        respawnPoint.position.y,
        Camera.main.transform.position.z
        );

    }
}
