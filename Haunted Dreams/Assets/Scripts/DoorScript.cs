using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{	
    public Text doorText;
    public AudioClip s_Door;
    Animator animator;
    public bool _TriggerActivated = false;
    public bool _DoorIsOpen;

	void Start ()
    { 		
		animator = GetComponent<Animator> ();
        _DoorIsOpen = false;
        doorText.text = " ";	
	}
    void Update()
    {       
        if (Input.GetKeyDown(KeyCode.E))
        {
            _DoorIsOpen = !_DoorIsOpen;
            GetComponent<AudioSource>().PlayOneShot(s_Door);
        }
        if (_TriggerActivated)
        {
            if (_DoorIsOpen)
            {
                animator.SetBool("isOpen", true);
                Debug.Log("I have Opened");
                doorText.text = "Press E to Close Door";
            }
            if (!_DoorIsOpen)
            {
                animator.SetBool("isOpen", false);
                Debug.Log("I have closed");
                doorText.text = "Press E to Open Door";
            }
        }
        else
        {
            doorText.text = " ";
        }
    }
}
