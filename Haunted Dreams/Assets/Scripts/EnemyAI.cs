using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemyAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE
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
        
        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

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
            state = EnemyAI.State.PATROL;

            alive = true;
            StartCoroutine("FSM");
        }
        IEnumerator FSM()
        {
            while(alive)
            {
                switch(state)
                {
                    case State.PATROL:
                        Patrol();
                        break;

                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null;
            }
        }
        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <=2)
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

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                state = EnemyAI.State.CHASE;
                target = coll.gameObject;
            }

        }  
    }

}
