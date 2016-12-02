using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUpMatches : MonoBehaviour
{
    public int _matchesAmount = 1;
    public RayCast_Pickup_Items item;

    void Start()
    {
        item = FindObjectOfType<RayCast_Pickup_Items>();
    }

    void OnTriggerStay(Collider mat)
    {
        if (mat.tag == "Player")
        {
            item.canHover = true;
        }
        else if (mat.tag != "Player")
        {
            item.canHover = false;
        }
    }

    public void AddMatch()
    {
        Checkpoint temp = GameObject.FindGameObjectWithTag("Checkpoint").
        GetComponentInChildren<Checkpoint>();
        temp.AddMatches(_matchesAmount);
        Destroy(gameObject);
    }


}

	
