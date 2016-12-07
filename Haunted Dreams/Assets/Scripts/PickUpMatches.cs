using UnityEngine;

public class PickUpMatches : MonoBehaviour
{
    public int _matchesAmount = 1;
    public LevelManager list;
    public bool pickedUp;
    public bool checkpointActivated;
    void Start()
    {
        list = FindObjectOfType<LevelManager>();
        pickedUp = false;
        checkpointActivated = false;
    }
    public void AddMatch()
    {
        Checkpoint temp = GameObject.FindGameObjectWithTag("Checkpoint").
        GetComponentInChildren<Checkpoint>();
        temp.AddMatches(_matchesAmount);
        gameObject.SetActive(false);
        if (pickedUp == false)
        {
            list.tempPickedUpMatches.Add(this);
            pickedUp = true;
        }
    }
}

	
