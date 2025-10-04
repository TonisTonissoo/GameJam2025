using UnityEngine;

public class keyslot : MonoBehaviour
{
    public GameObject[] lockpicks;  
    private int index = 0;          
    public bool lockpicks_aligned = true;

    void Update()
    {
        if (index<lockpicks.Length){
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (index < lockpicks.Length)
                    {
                        
                        lockpicks[index].GetComponent<movement>().stop_pick();
                        index++;
                    }
                }
        
            }
        else if (index == lockpicks.Length)
            {
                foreach (GameObject el in lockpicks)
                {
                    if (el.GetComponent<movement>().lock_open == false)
                    {
                        lockpicks_aligned = false;
                    }
                }
                if (lockpicks_aligned)
                {
                    Debug.Log("Good Job you opened the lock.");
                }
                index++;
                
            }
    }
}