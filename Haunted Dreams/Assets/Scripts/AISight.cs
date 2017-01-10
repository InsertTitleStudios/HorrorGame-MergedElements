using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class AISight : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    public bool isBoss = false;
    public float health = 1000f;
    public bool deductHealth = false;
    public bool collision = false;
    public enum State
    {
        PATROL,
        CHASE,
        INVESTIGATE,
        STUNNED
    }

    public State state;
    private bool alive;

    //Var for Patrolling
    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 0.5f;

    public bool isRandom;

    //Var for Chasing
    public float chaseSpeed = 1f;
    public GameObject target;

    //Var for Investigating
    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;
    public bool flashlight_is_on;

    //Var for Sight
    public float heightMultiplier;
    public float sightDist = 10;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        target = GameObject.FindGameObjectWithTag("Player");

        agent.updatePosition = true;
        agent.updateRotation = false;

        if (isRandom == true)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);
        }
        else
        {
            waypointInd = 0;
        }
        state = AISight.State.PATROL;

        alive = true;

        heightMultiplier = 1.36f;
        StartCoroutine("FSM");
    }
    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;

                case State.CHASE:
                    Chase();
                    break;

                case State.INVESTIGATE:
                    Investigate();
                    break;

                case State.STUNNED:
                    Stunned();
                    break;
            }
            yield return null;
        }
    }
    //Need to fix AI stop angling on the side
    // Need to fix AI investigating so it will chase player if it sees them in its range.
    //Need to fix AI collission with flashlight
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            if (isRandom == true)
            {
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                waypointInd++;
                if (waypointInd >= waypoints.Length)
                {
                    waypointInd = 0;
                }
            }
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        character.Move(agent.desiredVelocity, false, false);
    }

    void Investigate()
    {
        timer += Time.deltaTime;
        agent.SetDestination(this.transform.position);
        character.Move(Vector3.zero, false, false);
        
        transform.LookAt(investigateSpot);
      
        if (timer >= investigateWait)
        {
            state = AISight.State.PATROL;
            timer = 0;
        }
    }

    void Stunned()
    {
        character.Move(Vector3.zero, false, false); ;
    }

    void OnTriggerEnter(Collider coll)
    {
       if (coll.tag == "Player")
        {
            state = AISight.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
        }

        if (isBoss == true)
        {

            if (collision)
            {

                if (coll.tag == "HighBeam")
                {
                    state = AISight.State.STUNNED;
                }
                else
                {
                    state = AISight.State.PATROL;
                }
            }
        }

        else if (isBoss == false)
        {
            //  print("Collision is: " + collision + "Flashlight is also turned on.");
            if (collision)
            {
                if (coll.tag == "LowBeam")
                {
                    print("Collission with low beam: ");
                    deductHealth = true;
                }
                if (coll.tag == "HighBeam")
                {
                    print("Collission with: high beam ");
                    health = 0;
                }
                Health();
            }

            else if (!collision)
            {
                deductHealth = false;
                collision = false;
            }
                /* if (coll.tag != "LowBeam")
                 {
                     Debug.Log("No Collision");
                   //  deductHealth = false;
                 }*/
                
            
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = AISight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = AISight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = AISight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }

       
    }

    public void Health()
    {
        if (deductHealth)
        {
            health--;
        }


        if (health <= 0)
        {
            print("Deducting health" + health);
            health = 0;
            Destroy(gameObject);
        }
    }

    
}


