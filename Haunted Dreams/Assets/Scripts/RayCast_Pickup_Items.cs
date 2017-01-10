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
    public bool boolBeOn = false;
    public bool rangeMode;

    //Sonar Raycast Variables
    private float sonarRange = 10f; //Range of sonar raycast
    private float fov = 10f; //Angle of sonar
    private float angle;
    private int segments = 10; //Number of raycast lines
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

                if (rangeMode)
                {
                    sonarRange = 14f;
                }
                else if (!rangeMode)
                {
                    sonarRange = 10f;
                }

                if (Physics.Raycast(sonar.origin, targetPos, out hit, sonarRange))
                {
                    if (hit.collider.tag == "Small Enemy" || hit.collider.tag == "Large Enemy")
                    {
                       // Debug.Log("Enemy");
                        enemyHit = true;
                        EnemyHit();
                    }
                }
                enemyHit = false;
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
        print(enemyHit);

        if (flashlight_is_on)
        {
            //  Debug.Log("Hitting enemy I'll set collission to true");


            
            if (enemyHit)
            {
                print(beamCollission.collision);
                beamCollission.collision = true;
                boolBeOn = true;
                //  Debug.Log("I'm in this method");


            }
            if (!enemyHit)
            {
                //Debug.Log("Not hitting enemy so I'll turn off");
                beamCollission.deductHealth = false;
                beamCollission.collision = false;
                boolBeOn = false;
            }

        }
        if (!flashlight_is_on)
        {
            //Debug.Log("hitting enemy but light not on. I'll turn off collission");
            beamCollission.deductHealth = false;
            beamCollission.collision = false;
            boolBeOn = false;
        }

        
        //   else
        //  {
        // 
        // }
    }
        
}
