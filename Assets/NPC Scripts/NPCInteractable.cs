using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPCInteractable : MonoBehaviour
{
    public float NPCSelector;
    public float DialogueSelector;
    public float fshTalk;
    public GameObject DialogueBackground;
    public GameObject DialogueObject;
    public GameObject Fsh;
   
    private void Start()
    {
        
    }
    
    public void Interact()
    {
        
        
        Debug.Log("Interact");
        Time.timeScale = 0f;
        //NPCScript.Run();

        if (NPCSelector == 0)
        {
            PlayGame();
            Time.timeScale = 1f;
        }
        
        if (NPCSelector == 1)
        {
            
            
            if (fshTalk == 1)
            {
                Fsh.SetActive(true);
                DialogueBackground.SetActive(true);
                DialogueObject.SetActive(true);
                DialogueObject.GetComponent<TMPro.TextMeshProUGUI>().SetText("fsh.    (Pardon me my fine gentleman, These suspicious cephalopods have invaded the fountain plaza.)");
            }

            if (fshTalk == 2)
            {
                DialogueObject.GetComponent<TMPro.TextMeshProUGUI>().SetText("FSH.       (KILL THEM ALL.)");
                
            }

            if (fshTalk == 3)
            {
                DialougeExterminator();
            }

            if (fshTalk == 4)
            {
                Fsh.SetActive(true);
                DialogueBackground.SetActive(true);
                DialogueObject.SetActive(true);
                DialogueObject.GetComponent<TMPro.TextMeshProUGUI>().SetText("Fsh?    (What are you waiting for?)");
                
            }

            if (fshTalk == 5)
            {
                Fsh.SetActive(false);
                DialogueBackground.SetActive(false);
                DialogueObject.SetActive(false);
                fshTalk = 3;
                Time.timeScale = 1f;
            }
            
            
            
            
        }
        
        fshTalk += 1;
        
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void DialougeExterminator()
    {
        Fsh.SetActive(false);
        DialogueBackground.SetActive(false);
        DialogueObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
