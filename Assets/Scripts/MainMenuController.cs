using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("Level1");
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
        SceneManager.LoadScene("Settings");
    }
    public void OnExitButtonClicked()
    {
        Debug.Log("Game exiting...");
        Application.Quit();
    }
}
