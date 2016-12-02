using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    //public AudioClip _switch;
   // public AudioClip _batteryPickUp;

    public int _maximumBatteryPower = 100;
    public float _currentBatteryPower = 0f;

    // 2 Modes of flashlight Low = small intensity short range, large angle. High = large intensity, small angle but longer range.

    public float _lowPowerIntensityMode = 3f;
    public float _lowPowerSpotAngle = 40f;
    public float _lowPowerRange = 20f;

    public float _highPowerIntensityMode = 6f;
    public float _highPowerSpotAngle = 20;
    public float _highPowerRange = 30f;

    public float _lowDrainBatterySpeed = 2.5f;
    public float _highDrainBatterySpeed = 5f;

    public float _batteryBarLength;

    public float _maxFlickerSpeed = 1f;
    public float _minFlickerSpeed = 0.1f;

    // Use this for initialization
    void Start()
    { flashlight = GetComponentInChildren<Light>();
        flashlight.enabled = false;
        _currentBatteryPower = _maximumBatteryPower; }
	
	// Update is called once per frame
	void Update ()
    { _batteryBarLength = (Screen.width / 4) * (_currentBatteryPower / (float) _maximumBatteryPower);

       // if (Input.GetButtonDown("Flashlight"))
      //  { //GetComponent<AudioSource>().PlayOneShot(_switch);
            //flashlight.enabled = !flashlight.enabled; }

        if (flashlight.enabled)
        { FlashlightOn(); }

        if (_currentBatteryPower == 0)
        { StopCoroutine("FlashlightModifier");
            flashlight.enabled = false; }}

    private void FlashlightOn()
    { if (Input.GetMouseButton(1) && flashlight.enabled == true)
        { flashlight.intensity = _highPowerIntensityMode;
            flashlight.spotAngle = _highPowerSpotAngle;
            flashlight.range = _highPowerRange; }
        else
        { flashlight.intensity = _lowPowerIntensityMode;
            flashlight.spotAngle = _lowPowerSpotAngle;
            flashlight.range = _lowPowerRange; }

        if (flashlight.enabled)
        { _currentBatteryPower -= _lowDrainBatterySpeed * Time.deltaTime; }

        if (Input.GetMouseButton(1) && flashlight.enabled == true)
        { _currentBatteryPower -= _highDrainBatterySpeed * Time.deltaTime; }

        if(_currentBatteryPower <= 0)
        { _currentBatteryPower = 0; }

        if (_currentBatteryPower < 10)
        { StartCoroutine("FlashlightModifier"); }

        if (_currentBatteryPower > 10)
        { StopCoroutine("FlashlightModifier"); }}

    IEnumerator FlashlightModifier()
    { while (true)
        { flashlight.enabled = true;
            yield return new WaitForSeconds
                (Random.Range(_minFlickerSpeed, _maxFlickerSpeed));

            flashlight.enabled = false;
            yield return new WaitForSeconds
                (Random.Range(_minFlickerSpeed, _maxFlickerSpeed)); }}
    
    public void AddBattery(int _batteryPowerAmount)
    { _currentBatteryPower += _batteryPowerAmount;

        if (_currentBatteryPower >= _maximumBatteryPower)
        { _currentBatteryPower = _maximumBatteryPower; }

        // if (_batteryPickUp != null)
        //{ GetComponent<AudioSource>().clip = _batteryPickUp;
        //  GetComponent<AudioSource>().Play(); }}

        //void OnGUI()           // Change this to latest GUI in unity
        //{ GUI.Box(new Rect(5, 35, _batteryBarLength, 20), "Battery"); 
    }
}
