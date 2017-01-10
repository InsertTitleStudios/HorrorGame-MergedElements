using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class Enemy : MonoBehaviour {

    public GameObject player;
    public AudioClip[] footsounds;
    public Transform eyes;
    public AudioSource growl;
    public GameObject deathCam;
    public Transform camPos;

    private NavMeshAgent nav;
    private AudioSource sound;
    private Animator anim;
    private string state = "idle";
    private bool alive = true;
    private float wait = 0f;
    private bool highAlert = false;
    private float alertness = 20f;

    public GameObject[] waypoints;
    private int waypointInd;
    public bool isRandom;

    public KillPlayer dead;


    void Start()
    {
        dead = FindObjectOfType<KillPlayer>();
        nav = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nav.speed = 1.2f;
        anim.speed = 1.2f;

        if (isRandom == true)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);
        }
        else
        {
            waypointInd = 0;
        }

    }

    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    // Check if see player
    public void CheckSight()
    {
        if (alive)
        {
            RaycastHit hit;
            if (Physics.Linecast(eyes.position, player.transform.position, out hit))
            {
                if (hit.collider.gameObject.name == "Player")
                {
                    if (state != "kill")
                    {
                        state = "chase";
                        nav.speed = 3.5f;
                        anim.speed = 3.5f;
                        growl.pitch = 1.2f;
                        growl.Play();
                    }
                }
            }
        }

    }

    void Update()
    {
  //      Debug.DrawLine(eyes.position, player.transform.position, Color.green);

        if (alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            // Idle //
            if (state == "idle")
            {
                // picks random place to walk to - I can change this to patrol waypoints later
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                NavMeshHit navHit;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);

                if (highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                    // each time, lose awareness of player general position
                    alertness += 5f;

                    if (alertness > 20f)
                    {
                        highAlert = false;
                        nav.speed = 1.2f;
                        anim.speed = 1.2f;
                    }
                }

                nav.SetDestination(navHit.position);
                state = "walk";

            }

            // Walk //
            if (state == "walk")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                }
            }
            // Search | Investigate //
            if (state == "search")

            {
                if (wait > 0f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0f, 120f * Time.deltaTime, 0f);
                }
                else
                {
                    state = "idle";
                }
            }

            // Chase //
            if (state == "chase")
            {
                nav.destination = player.transform.position;

                // lose sight of player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > 10f)
                {
                    state = "hunt";
                }
                // Kill the player
                else if (nav.remainingDistance <= nav.stoppingDistance + 1f && !nav.pathPending)
                {
                    if (player.GetComponent<PlayerScript>().alive)
                    {
                        state = "kill";
                        player.GetComponent<PlayerScript>().alive = false;
                        player.GetComponent<FirstPersonController>().enabled = false;
                        deathCam.SetActive(true);
                        deathCam.transform.position = Camera.main.transform.position;
                        deathCam.transform.rotation = Camera.main.transform.rotation;
                        Camera.main.gameObject.SetActive(false);
                        growl.pitch = 0.7f;
                        growl.Play();
                        Invoke("Reset", 5f);
                    }
                }
            }

            // Hunt //
            if (state == "hunt")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                    highAlert = true;
                    alertness = 5f;
                    CheckSight();
                }
            }

            // Kill //
            if (state == "kill")
            {
                deathCam.transform.position = Vector3.Slerp(deathCam.transform.position, camPos.position, 10f * Time.deltaTime);
                deathCam.transform.rotation = Quaternion.Slerp(deathCam.transform.rotation, camPos.rotation, 10f * Time.deltaTime);
                anim.speed = 1f;
                nav.SetDestination(deathCam.transform.position);
            }
          //  nav.SetDestination(player.transform.position);
        }
        

    }

    void Reset()
    { // Access kill Player script and set to respawn player
        //dead
    }

    //die//
    public void Death()
    {
        anim.SetTrigger("dead");
        anim.speed = 1f;
        alive = false;
        nav.Stop();
    }
}
