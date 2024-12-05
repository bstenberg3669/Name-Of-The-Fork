using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

static var Paused = false;
public class PauseSaddness : MonoBehaviour
{
    public static bool paused = false;
    
    public GameObject pauseMenu;
    
    public GameObject pauseMen;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

  
    void Resume()
    {
        pauseMenu.SetActive(false);
        pauseMen.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        pauseMen.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
    }
}
