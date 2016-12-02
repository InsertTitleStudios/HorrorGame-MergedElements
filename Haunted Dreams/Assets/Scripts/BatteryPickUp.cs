using UnityEngine;
using System.Collections;

public class BatteryPickUp : MonoBehaviour
{

    public int _batteryPowerAmount = 50;
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

    public void AddBatteries()
    {
        Flashlight temp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Flashlight>();
        temp.AddBattery(_batteryPowerAmount);
        Destroy(gameObject);
    }

}


