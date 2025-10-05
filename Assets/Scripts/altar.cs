using UnityEngine;
using UnityEngine.SceneManagement;

public class altar : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
