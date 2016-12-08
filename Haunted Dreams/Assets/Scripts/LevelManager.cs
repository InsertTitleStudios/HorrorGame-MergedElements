using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private FirstPersonController player;
    public Checkpoint match;
    public Flashlight flashlight;
    [HideInInspector]
    public List<PickUpMatches> tempPickedUpMatches;
    public List<BatteryPickUp> tempPickedUpBatteries;
    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        match = FindObjectOfType<Checkpoint>();
    }
    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
        match.respawn = true;
        flashlight.respawn = true;
        for (int i = tempPickedUpMatches.Count - 1; i >= 0; i--)
        {
            if (tempPickedUpMatches[i].checkpointActivated && tempPickedUpMatches[i].pickedUp)
            { }
            else
            {
                tempPickedUpMatches[i].gameObject.SetActive(true);
                tempPickedUpMatches[i].pickedUp = false;
                tempPickedUpMatches.Remove(tempPickedUpMatches[i]);
            }
        }
        for (int i = tempPickedUpBatteries.Count - 1; i >= 0; i--)
        {
            if (tempPickedUpBatteries[i].checkpointActivated && tempPickedUpBatteries[i].pickedUp)
            { }
            else
            {
                tempPickedUpBatteries[i].gameObject.SetActive(true);
                tempPickedUpBatteries[i].pickedUp = false;
                tempPickedUpBatteries.Remove(tempPickedUpBatteries[i]);
            }
        }
    }
} 


