using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        //SceneManager.LoadScene("GameScene");
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
    }
    public void OnExitButtonClicked()
    {
        Debug.Log("Game exiting...");
        //Application.Quit();
    }
}
