using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public LevelManager levelManager;
    public int playerHealth = 30;
    private int smallEnemyDamage = 10;
    private int largeEnemyDamage = 30;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); }
	
	void Update ()
    { if (playerHealth <= 0)
        { playerHealth = 0;
          levelManager.RespawnPlayer();
          playerHealth = 30; }}

    void OnCollisionEnter(Collision other) //Fix Collision - Doesn't work however health function works when done manually
    { if (other.gameObject.tag =="Small Enemy")
        { print("enemy just touched" + playerHealth);
          playerHealth -= smallEnemyDamage; }

     if (other.gameObject.tag == "Large Enemy")
        { print("enemy just touched" + playerHealth);
          playerHealth -= largeEnemyDamage; }}}
