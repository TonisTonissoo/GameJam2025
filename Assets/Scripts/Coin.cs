using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1; // mitu punkti see coin annab

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // veendu, et mängijal on Tag = "Player"
        {
            // Teavitame PlayerControllerit või GameManagerit
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddCoins(coinValue);
            }

            // Hävita coin pärast korjamist
            Destroy(gameObject);
        }
    }
}
