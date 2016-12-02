using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private Animator _animator = null;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Collider)
	{
		if (Collider.gameObject.tag == "Player") {
			_animator.SetBool ("isopen", true);
		}
	}

	void OnTriggerExit(Collider Collider)
	{
		_animator.SetBool ("isopen", false);
	}
}
