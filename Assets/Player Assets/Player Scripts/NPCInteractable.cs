using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public float NPCSelector;
    public float DialogueSelector;
    public GameObject DialogueBackground;
    public GameObject DialogueObject;
    public GameObject Fsh;
   
    private void Start()
    {
        
    }
    
    public void Interact()
    {
        
        Debug.Log("Interact");
        
        //NPCScript.Run();

        if (NPCSelector == 1)
        {
            if (DialogueSelector == 1)
            {
                DialogueObject.GetComponent<TMPro.TextMeshProUGUI>().SetText("fsh.    (Pardon me my fine gentleman, These suspicious cephalopods have invaded the fountain plaza.)");
                
            }

            if (DialogueSelector == 2)
            {
                DialogueObject.GetComponent<TMPro.TextMeshProUGUI>().SetText("FSH.       (KILL THEM ALL.)");
            }
            Fsh.SetActive(true);
            DialogueBackground.SetActive(true);
            DialogueObject.SetActive(true);
        }
        
        DialogueSelector += 1;
        
    }
}
