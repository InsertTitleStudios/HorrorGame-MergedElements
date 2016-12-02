using UnityEngine;
using System.Collections;

public class Crosshair_Manager : Singleton_Crosshair_UI<Crosshair_Manager> {

    public bool hover = false;

    public void ChangeCrosshair()
    {
        if (hover == true)
        {
            Debug.Log("True");
        }

        else if (hover == false)
        {
            Debug.Log("False");
        }
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
