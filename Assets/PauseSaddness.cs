using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseSaddness : MonoBehaviour
{
    public static bool paused = false;
    
    public GameObject pauseMenu;
    public GameObject Retticle;

    
    
    


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

  
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Retticle.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        paused = false;
    }

    public void Settings()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/Scenes 1/MainMenu");
        
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Retticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        paused = true;
    }
}
