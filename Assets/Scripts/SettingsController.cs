using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {

        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("masterVolume");
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
        else
        {
            volumeSlider.value = 0.25f;
            AudioListener.volume = 0.25f;
            PlayerPrefs.SetFloat("masterVolume", 0.25f);
            PlayerPrefs.Save();
        }


        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button pressed");
        SceneManager.LoadScene("MainMenu");
    }
}
