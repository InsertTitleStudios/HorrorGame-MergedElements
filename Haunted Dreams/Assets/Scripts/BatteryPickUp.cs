using UnityEngine;
using System.Collections;

public class BatteryPickUp : MonoBehaviour
{
    public int _batteryPowerAmount = 50;
    public LevelManager list;
    public bool pickedUp;
    public bool checkpointActivated;

    void Start()
    {
        list = FindObjectOfType<LevelManager>();
        pickedUp = false;
        checkpointActivated = false;
    }
    public void AddBatteries()
    {
        Flashlight temp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Flashlight>();
        temp.AddBattery(_batteryPowerAmount);
        gameObject.SetActive(false);
        if (pickedUp == false)
        {
            list.tempPickedUpBatteries.Add(this);
            pickedUp = true;
        }
    }
}


