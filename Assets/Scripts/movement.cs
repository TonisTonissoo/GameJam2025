using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Vector2 orbitCenter = Vector2.zero;   
    public float orbitRadius = 0.5f;             
    public float orbitSpeed = 90f;               
    private bool button_press = false;
    private float currentAngle = 0f;        
    public bool lock_open = false;     
    private SpriteRenderer spriteRenderer;        

    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start at the rightmost point (0 degrees)
        currentAngle = 0f;
        UpdateOrbit();
    }

    public void stop_pick()
    {
        button_press=true;
    }

    void Update()
    {   
        
        if (!button_press){// Increase angle over time
        currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        UpdateOrbit();
        }
        else
        {
            orbitSpeed = 0;
        }

    }

    void UpdateOrbit()
    {
        // Convert angle to radians
        float rad = currentAngle * Mathf.Deg2Rad;

        // Position on circle
        float x = orbitCenter.x + Mathf.Cos(rad) * orbitRadius;
        float y = orbitCenter.y + Mathf.Sin(rad) * orbitRadius;

        transform.position = new Vector3(x, y, transform.position.z);

        // --- ROTATION ---
        Vector2 direction = new Vector2(-Mathf.Sin(rad), Mathf.Cos(rad)); 
        float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>() != null)
        {
            SetColor(Color.green); // Change color when overlapping
            Debug.Log("green");
            lock_open = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>() != null)
        {
            SetColor(Color.blue); // Change color when overlapping
            Debug.Log("blue");
            lock_open = true;
        }
        
    }
  
    // --- Helper function to change color ---
    private void SetColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }
}


