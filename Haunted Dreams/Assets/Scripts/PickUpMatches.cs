using UnityEngine;

public class PickUpMatches : MonoBehaviour
{
    public int _matchesAmount = 1;
    public void AddMatch()
    {
        Checkpoint temp = GameObject.FindGameObjectWithTag("Checkpoint").
        GetComponentInChildren<Checkpoint>();
        temp.AddMatches(_matchesAmount);
        Destroy(gameObject);
    }
}

	
