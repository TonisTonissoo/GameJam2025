using TMPro;
using UnityEngine;

public class keyslot : MonoBehaviour
{
    public GameObject[] lockpicks;  
    private int index = 0;          
    public bool lockpicks_aligned = true;

    [Header("UI")]
    [SerializeField] private TMP_Text endText;    // Drag TMP tekst siia
    [SerializeField] private GameObject endPanel;

    void Update()
    {
        if (index < lockpicks.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lockpicks[index].GetComponent<movement>().stop_pick();
                index++;
            }
        }
        else if (index == lockpicks.Length)
        {
            lockpicks_aligned = true; // reset kontrolliks

            foreach (GameObject el in lockpicks)
            {
                if (el.GetComponent<movement>().lock_open == false)
                {
                    lockpicks_aligned = false;
                    break;
                }
            }

            if (lockpicks_aligned)
            {
                ShowEndMessage("You won 1 000 000 coins!");
            }
            else
            {
                ShowEndMessage("Unlucky");
            }

            index++;
        }
    }

    private void ShowEndMessage(string message)
    {
        if (endText != null)
        {
            endText.text = message;
            endText.gameObject.SetActive(true);
        }

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        Debug.Log(message);
        Time.timeScale = 0f;
    }
}
