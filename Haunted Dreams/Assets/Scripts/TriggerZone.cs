using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour {

    public DoorScript door;

    void Start()
    {
        door = FindObjectOfType<DoorScript>();
    }

    void OnTriggerStay(Collider mat)
    {
        if (mat.tag == "Player")
        {
            door._TriggerActivated = true;
        }
    }

    void OnTriggerExit(Collider mat)
    {
        if (mat.tag == "Player")
        {
            door._TriggerActivated = false;
        }
    }
}
