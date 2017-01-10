using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public bool alive = true;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "eyes")
        {
            other.transform.parent.GetComponent<Enemy>().CheckSight();
        }
    }    
}
