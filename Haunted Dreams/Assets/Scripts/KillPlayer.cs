﻿using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    public LevelManager levelManager;
    public int playerHealth = 250;
    private int smallEnemyDamage = 1;
    private int largeEnemyDamage = 250;

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
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Small Enemy")
        {
            playerHealth -= smallEnemyDamage;
        }
        if (other.gameObject.tag == "Large Enemy")
        {
            playerHealth -= largeEnemyDamage;
        }
    }
}
