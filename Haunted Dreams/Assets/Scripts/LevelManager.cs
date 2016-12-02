using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;   
    private FirstPersonController player;
    public Checkpoint match;

    void Start ()
    { player = FindObjectOfType<FirstPersonController>();
        match = FindObjectOfType<Checkpoint>();
    }
	
    public void RespawnPlayer()
    { Debug.Log("Respawned Player");
      player.transform.position = currentCheckpoint.transform.position;
        match.respawn = true;
    } }


