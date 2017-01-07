using UnityEngine;
using System.Collections;

public class RayCast_Pickup_Items : MonoBehaviour
{

    //Pick up objects Raycast Variables
    private float pickRange = 50f;
    public bool flashlight_is_on = false;
    public bool canHover = false;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public Camera cam;
    public AISight beamCollission;
    public AudioClip collectSound;

    //Sonar Raycast Variables
    private float sonarRange = 5f; //Range of sonar raycast
    private float fov = 15f; //Angle of sonar
    private float angle;
    private int segments = 30; //Number of raycast lines
    private Vector3 targetPos;
    public bool enemyHit = false;

    void Start()
    {
        _CrossHairImage.SetActive(true);
        _HandImage.SetActive(false);
        beamCollission = FindObjectOfType<AISight>();
        targetPos = Vector3.zero;
        StartCoroutine("RaySonar");
    }

    IEnumerator RaySonar()
    {
        while (true)
        {
            for (int i = -segments; i < segments; i++)
            {
                RaycastHit hit;
                Ray sonar = cam.ViewportPointToRay(new Vector3(0.5f, -1.5f, 0.5f));
                float segmentIndex = Mathf.Abs(i);
                angle = Mathf.Lerp(-fov / 2, fov / 2, segmentIndex / segments);
                yield return null;
                targetPos = (Quaternion.Euler(0, angle, 0) * transform.forward).normalized * sonarRange;
                if (Physics.Raycast(sonar.origin, targetPos, out hit))
                {
                    if (hit.collider.tag == "Small Enemy")
                    {
                        Debug.Log("Enemy");
                        enemyHit = true;
                    }
                    else if (hit.collider.tag == "Wall")
                    {
                        enemyHit = false;
                        Debug.Log("Wall");
                    }
                    EnemyHit();
                }
                Debug.DrawRay(sonar.origin, targetPos, Color.red, 0.5f);
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray pickup = cam.ViewportPointToRay(new Vector3(.5f, .5f, .5f));

        if (Physics.Raycast(pickup, out hit, pickRange))
        {
            Debug.DrawRay(pickup.origin, pickup.direction, Color.green);
            if (canHover == true)
            {
                if (hit.collider.tag == "Matchbox" || hit.collider.tag == "Battery")
                {
                    _HandImage.SetActive(true);
                    _CrossHairImage.SetActive(false);
                    Debug.Log("Hit match");

                    if (Input.GetButton("Fire1"))
                    {
                        if (hit.collider.tag == "Matchbox")
                        {
                            hit.collider.gameObject.GetComponent<PickUpMatches>().AddMatch();
                            canHover = false;
                            _HandImage.SetActive(false);
                            GetComponent<AudioSource>().PlayOneShot(collectSound);
                            _CrossHairImage.SetActive(true);
                        }
                        else if (hit.collider.tag == "Battery")
                        {
                            hit.collider.gameObject.GetComponent<BatteryPickUp>().AddBatteries();
                            GetComponent<AudioSource>().PlayOneShot(collectSound);
                            canHover = false;
                            _HandImage.SetActive(false);
                            _CrossHairImage.SetActive(true);
                        }
                    }
                }
                else
                {
                    _HandImage.SetActive(false);
                    _CrossHairImage.SetActive(true);
                }
            }
        }
        

    }

    public void EnemyHit()
    {
       
        if (enemyHit)
        {
          //  Debug.Log("I'm in this method");
            if (flashlight_is_on)
            {
                beamCollission.collision = true;
            }
            else
            {
                beamCollission.deductHealth = false;
                beamCollission.collision = false;
            }
        }
    }
        
}
