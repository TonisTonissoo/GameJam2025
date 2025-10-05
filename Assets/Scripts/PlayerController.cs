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

    [Header("Jump Settings")]
    [SerializeField] private int maxJumps = 2;    // mitu hüpet max (1 = tavaline, 2 = double jump)

    private int jumpCount = 0;


    private Rigidbody2D rb;
    private bool isGrounded;
    private float fallVelocity;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Horisontaalne liikumine
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (move < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Hüppamine (sh double jump)
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            //Nullime vertikaalse kiiruse enne teist hüpet → resetib kukkumise
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // Rakendame hüppe jõu
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpCount++;
        }

        // Salvestame hetke vertikaalkiiruse kukkumise kontrolliks
        fallVelocity = rb.linearVelocity.y;
        anim.SetBool("walk", move != 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                // Esmalt märgime grounded ära
                isGrounded = true;

                jumpCount = 0;

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
        Debug.Log($"Player died");
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        Camera.main.transform.position = new Vector3(
        respawnPoint.position.x,
        respawnPoint.position.y,
        Camera.main.transform.position.z
        );

    }
}
