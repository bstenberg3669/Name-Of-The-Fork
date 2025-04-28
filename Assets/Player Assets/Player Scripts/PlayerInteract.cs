using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCam;

    private float range = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name.Contains("NPC"))
                {
                    hit.collider.GetComponent<NPCInteractable>().Interact();
                }
            }
        }
    }
}
