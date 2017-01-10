using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    public LevelManager levelManager;
    public int playerHealth = 250;
    private int smallEnemyDamage = 1;
    public bool smallDamage = false;
    public bool largeDamage = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            playerHealth = 250;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Small Enemy")
        {
            smallDamage = true;
            Health();

        }
        if (other.tag == "Large Enemy")
        {
            largeDamage = true;
            Health();
        }       
        
        
        smallDamage = false;
        largeDamage = false;
    }

    public void Health()
    {
        if (smallDamage)
        {
            playerHealth-= smallEnemyDamage;
        }
        if (largeDamage)
        {
            playerHealth = 0;
        }
        
    }
    
}
