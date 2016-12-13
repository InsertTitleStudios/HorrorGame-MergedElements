using UnityEngine;
using System.Collections;

public class MoveOnPath : MonoBehaviour {

    public AI_Paths _PathToFollow;

    public int _currentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 last_position;
    Vector3 current_position;


	// Use this for initialization
	void Start ()
    {
        _PathToFollow = GameObject.Find(pathName).GetComponent<AI_Paths>();
        last_position = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(_PathToFollow.path_objs[_currentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _PathToFollow.path_objs[_currentWayPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(_PathToFollow.path_objs[_currentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            _currentWayPointID++;
        }
        if (_currentWayPointID >= _PathToFollow.path_objs.Count)
        {
            _currentWayPointID = 0;
        }
	}
}
