using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSystem : MonoBehaviour
{
    bool player_detection = false;


    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.E))
        {
            player_detection = false;
            Debug.Log("Player detection");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
