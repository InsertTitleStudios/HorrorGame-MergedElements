using UnityEngine;
using System.Collections;

public class TriggerArea : MonoBehaviour {

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
    }

    void OnTriggerExit(Collider mat)
    {
        if (mat.tag == "Player")
        {
            item.canHover = false;
        }
    }
}
