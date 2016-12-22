using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BatteryHud : MonoBehaviour {

    private float fillAmount;

    [SerializeField]
    private Image content;

    private Flashlight battery;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private float lerpSpeed;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
           // valueText.text = value + " %";
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        HUD();
	}

    private void HUD()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
                
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

}
