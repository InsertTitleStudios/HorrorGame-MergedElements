using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    public AudioClip _switch;

    public static int _maximumBatteryPower = 1050;
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
    

    public float _maxFlickerSpeed = 1f;
    public float _minFlickerSpeed = 0.1f;

    
    public LevelManager manager;
    public bool checkpointActivated = false;

//    [SerializeField]
   // private Stat batteryPower;

    public Text amountText;

    public GameObject _lowIntensityBeam;

    public RayCast_Pickup_Items casting;
    public GameObject _highIntensityBeam;

    public bool _modeChange = false;
    public bool _pause = false;
    public bool _batteryDead = false;
    public bool _batteryDying = false;
    public bool flicker = false;
    public bool respawn = false;
    public RayCast_Pickup_Items mode;

    public Image currentBatteryPower;

   /* private void Awake()
    {
        
    }*/
    void Start()
    {
     //   batteryPower.Initialize();
        UpdateBatteryHud();
        flashlight = GetComponentInChildren<Light>();
        _lowIntensityBeam.GetComponent<GameObject>();
        _highIntensityBeam.GetComponent<GameObject>();
        _lowIntensityBeam.gameObject.SetActive(false);
        _highIntensityBeam.gameObject.SetActive(false);
        flashlight.enabled = false;
        
        _currentBatteryPower = _maximumBatteryPower;
      //  batteryPower.MaxVal = _currentBatteryPower;
       // batteryPower.CurrentVal = _currentBatteryPower;
        _tempBatteryPower = _currentBatteryPower;
        mode = FindObjectOfType<RayCast_Pickup_Items>();
        casting = FindObjectOfType<RayCast_Pickup_Items>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            GetComponent<AudioSource>().PlayOneShot(_switch);
            flashlight.enabled = !flashlight.enabled;
            
            //casting.flashlight_is_on = flashlight;
            if (_batteryDead && _currentBatteryPower <= 0)
            {
                Dead();
            }
            if (_pause && _currentBatteryPower <= 50)
            {
                flashlight.enabled = false;
                flicker = false;
                Flicker();
                _pause = false;
            }
            if (checkpointActivated == true)
            {
                _tempBatteryPower = _currentBatteryPower;
            }

        }
        if (flashlight.enabled)
        {
            casting.flashlight_is_on = true;
            FlashlightOn();
            PowerCheck();
           
            ModeCheck();
        }
        else if (!flashlight.enabled)
        {
            casting.flashlight_is_on = false;
            FlashlightOff();           
        }
        if (respawn == true)
        {
            _currentBatteryPower = _tempBatteryPower;
           // batteryPower.CurrentVal = _tempBatteryPower;
            respawn = false;
        }
    }  
    private void Dying()
    {
        if (_batteryDying)
        {
            _pause = true;        
            flicker = true;         
        }
        else
        {
            flicker = false;
        }
        Flicker();
    }
    private void ModeCheck()
    {
        if (!_modeChange)
        {
            _lowIntensityBeam.gameObject.SetActive(true);
            _currentBatteryPower -= _lowDrainBatterySpeed * Time.deltaTime;
         //   batteryPower.CurrentVal -= _lowDrainBatterySpeed;
        }
        else if (_modeChange)
        {
            _currentBatteryPower -= _highDrainBatterySpeed * Time.deltaTime;
          //  batteryPower.CurrentVal -= _highDrainBatterySpeed;
        }
    }
    private void Dead()
    {
        _batteryDying = false;
        flashlight.enabled = false;
        flicker = false;
        Flicker();
        _lowIntensityBeam.gameObject.SetActive(false);
        _highIntensityBeam.gameObject.SetActive(false);
        _currentBatteryPower = 0;
    }
    private void Flicker()
    {
        if (flicker)
        {
            StartCoroutine("FlashlightModifier");
        }
        else
        {
            StopCoroutine("FlashlightModifier");
        }
    }
    private void PowerCheck()
    {
        if (_currentBatteryPower < 50)
        {
            _batteryDying = true;
            Dying();
        }
        if (_currentBatteryPower > 50)
        {
            _batteryDying = false;
            Dying();
        }
        if (_currentBatteryPower < 0)
        {
            _batteryDead = true;
            _currentBatteryPower = 0;
            Dead();
        }
    }
    private void FlashlightOn()
    {
        if (Input.GetMouseButtonDown(1) && !_modeChange)
        {
            mode.rangeMode = true;
            _modeChange = true;
            flashlight.intensity = _highPowerIntensityMode;
            flashlight.spotAngle = _highPowerSpotAngle;
            flashlight.range = _highPowerRange;
            _lowIntensityBeam.gameObject.SetActive(false);
            _highIntensityBeam.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(1) && _modeChange)
        {
            mode.rangeMode = false;
            _modeChange = false;
            flashlight.intensity = _lowPowerIntensityMode;
            flashlight.spotAngle = _lowPowerSpotAngle;
            flashlight.range = _lowPowerRange;
            _lowIntensityBeam.gameObject.SetActive(true);
            _highIntensityBeam.gameObject.SetActive(false);
        }
    }
    private void FlashlightOff()
    {
        _lowIntensityBeam.gameObject.SetActive(false);
        _highIntensityBeam.gameObject.SetActive(false);
        flashlight.intensity = _lowPowerIntensityMode;
        flashlight.spotAngle = _lowPowerSpotAngle;
        flashlight.range = _lowPowerRange;
        _modeChange = false;
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
    {
        _currentBatteryPower += _batteryPowerAmount;
       // batteryPower.CurrentVal += _batteryPowerAmount;
        if (_currentBatteryPower >= _maximumBatteryPower)
        {
            _currentBatteryPower = _maximumBatteryPower;
            
        }
    }

    private void UpdateBatteryHud()
    {
        float ratio = _currentBatteryPower / _maximumBatteryPower;
        currentBatteryPower.rectTransform.localScale = new Vector3(ratio, 1, 1);
        amountText.text = (ratio * 100).ToString() + '%';
    }

    
}
