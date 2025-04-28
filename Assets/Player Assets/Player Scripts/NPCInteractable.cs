using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public float dialougeSelector;
    private void Start()
    {
        
    }
    
    public void Interact()
    {
        Debug.Log("Interact");
        
        NPCScript.Run();
        
    }
}
