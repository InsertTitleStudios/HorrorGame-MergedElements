using UnityEngine;
using System.Collections;

public class BatteryPickUp : MonoBehaviour
{
    public int _batteryPowerAmount = 50;  
    public void AddBatteries()
    {
        Flashlight temp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Flashlight>();
        temp.AddBattery(_batteryPowerAmount);
        Destroy(gameObject);
    }
}


