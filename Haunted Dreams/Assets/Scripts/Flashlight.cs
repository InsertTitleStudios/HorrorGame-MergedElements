﻿using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    //public AudioClip _switch;
   // public AudioClip _batteryPickUp;

    public int _maximumBatteryPower = 1000;
    public static float _currentBatteryPower = 0f;
    public static float _tempBatteryPower = 0f;

    public float _lowPowerIntensityMode = 3f;
    public float _lowPowerSpotAngle = 40f;
    public float _lowPowerRange = 20f;

    public float _highPowerIntensityMode = 10f;
    public float _highPowerSpotAngle = 20;
    public float _highPowerRange = 30f;

    public float _lowDrainBatterySpeed = 2.5f;
    public float _highDrainBatterySpeed = 5f;

    public float _batteryBarLength;
    public bool _modeChange = false;

    public float _maxFlickerSpeed = 1f;
    public float _minFlickerSpeed = 0.1f;

    public bool respawn = false;
    public LevelManager manager;
    public bool checkpointActivated = false;

    public GameObject _lowIntensityBeam;
    public GameObject _highIntensityBeam;

    void Start()
    {
        flashlight = GetComponentInChildren<Light>();
        _lowIntensityBeam.GetComponent<GameObject>();
        _highIntensityBeam.GetComponent<GameObject>();
        _lowIntensityBeam.gameObject.SetActive(false);
        _highIntensityBeam.gameObject.SetActive(false);
        flashlight.enabled = false;
        _currentBatteryPower = _maximumBatteryPower;
        _tempBatteryPower = _currentBatteryPower;
    }
    void Update()
    {
        _batteryBarLength = (Screen.width / 4) * (_currentBatteryPower / (float)_maximumBatteryPower);
        if (Input.GetButtonDown("Flashlight"))
        { //GetComponent<AudioSource>().PlayOneShot(_switch);
            flashlight.enabled = !flashlight.enabled;
        }
        if (flashlight.enabled)
        {
            FlashlightOn();
            if (checkpointActivated == true)
            {
                _tempBatteryPower = _currentBatteryPower;
            }
            if (_modeChange == false)
            {
                _lowIntensityBeam.gameObject.SetActive(true);
                _currentBatteryPower -= _lowDrainBatterySpeed * Time.deltaTime;
            }
            else if (_modeChange == true)
            {
                _currentBatteryPower -= _highDrainBatterySpeed * Time.deltaTime;
            }
        }
        else if (!flashlight.enabled)
        {
            _lowIntensityBeam.gameObject.SetActive(false);
            _highIntensityBeam.gameObject.SetActive(false);
            flashlight.intensity = _lowPowerIntensityMode;
            flashlight.spotAngle = _lowPowerSpotAngle;
            flashlight.range = _lowPowerRange;
            _modeChange = false;
        }
        if (_currentBatteryPower == 0)
        {
            StopCoroutine("FlashlightModifier");
            flashlight.enabled = false;
            _lowIntensityBeam.gameObject.SetActive(false);
            _highIntensityBeam.gameObject.SetActive(false);
        }
        if (respawn == true)
        {
            _currentBatteryPower = _tempBatteryPower;
            respawn = false;
        }
    }
    private void FlashlightOn()
    {
        if (flashlight.enabled)
        {
            if (Input.GetMouseButtonDown(1) && _modeChange == false)
            {
                _modeChange = true;
                flashlight.intensity = _highPowerIntensityMode;
                flashlight.spotAngle = _highPowerSpotAngle;
                flashlight.range = _highPowerRange;
                _lowIntensityBeam.gameObject.SetActive(false);
                _highIntensityBeam.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonDown(1) && _modeChange == true)
            {
                _modeChange = false;
                flashlight.intensity = _lowPowerIntensityMode;
                flashlight.spotAngle = _lowPowerSpotAngle;
                flashlight.range = _lowPowerRange;
                _lowIntensityBeam.gameObject.SetActive(true);
                _highIntensityBeam.gameObject.SetActive(false);
            }
        }
        if (_currentBatteryPower <= 0)
        { _currentBatteryPower = 0; }

        if (_currentBatteryPower < 10)
        { StartCoroutine("FlashlightModifier"); }

        if (_currentBatteryPower > 10)
        { StopCoroutine("FlashlightModifier"); }
    }
    IEnumerator FlashlightModifier()
    {
        while (true)
        {
            flashlight.enabled = true;
            yield return new WaitForSeconds
                (Random.Range(_minFlickerSpeed, _maxFlickerSpeed));

            flashlight.enabled = false;
            yield return new WaitForSeconds
                (Random.Range(_minFlickerSpeed, _maxFlickerSpeed));
        }
    }
    public void AddBattery(int _batteryPowerAmount)
    { _currentBatteryPower += _batteryPowerAmount;

        if (_currentBatteryPower >= _maximumBatteryPower)
        { _currentBatteryPower = _maximumBatteryPower; }

        // if (_batteryPickUp != null)
        //{ GetComponent<AudioSource>().clip = _batteryPickUp;
        //  GetComponent<AudioSource>().Play(); }}
    }
}
