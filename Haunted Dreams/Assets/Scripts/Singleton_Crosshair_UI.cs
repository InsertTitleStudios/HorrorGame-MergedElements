using UnityEngine;
using System.Collections;

public class Singleton_Crosshair_UI<T> : MonoBehaviour where T : Component {

    private static T _instance;
    public static T instance
    {
        get {
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                _instance = Camera.main.gameObject.AddComponent<T>();
            }
            return _instance;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
