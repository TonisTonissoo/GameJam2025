using UnityEngine;

public class altar : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // Set this in the Inspector

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
