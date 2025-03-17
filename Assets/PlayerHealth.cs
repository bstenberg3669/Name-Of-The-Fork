using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100f;

    public float maxHealth = 150f;

    // Update is called once per frame
    void Update()
    {
        OverHeal();//Allows the player to gain up to 50 extra health for a total of 150
        OverHealthDecayTimer();//Counts time before starting over health decay
        HealthCap();//Ensures the player never has more than 150 health
        Death(); //Kills the player
    }
    
    public void OverHeal()
    {
        if (playerHealth > 100)
        {
            if (playerHealth > maxHealth)
            {
                playerHealth = 150;
            }
        }
    }

    public void OverHealthDecayTimer()
    {
        float decay = 0f;

        if (playerHealth > 100f)
        {
            decay += Time.deltaTime;
            if (decay >= 7.5f)
            {
                HealthDecay();
            }
            
        }
    }

    public void HealthDecay() //Decays Player health if it is greater than 100
    {
        while (playerHealth > 100f)
        {
            playerHealth -= 1f;
        }
    }

    public void HealthCap()
    {
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
    }
    public void Death()
    {
        if (playerHealth <= 0)
        { 
            Time.timeScale = 0;
            Debug.Log("You Died (lmao)");
            Application.Quit();
        }
    }
    
}
