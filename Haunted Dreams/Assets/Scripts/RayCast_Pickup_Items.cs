using UnityEngine;
using System.Collections;

public class RayCast_Pickup_Items : MonoBehaviour
{
    private float range = 50f;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public Camera cam;
    public AISight beamCollission;
    public bool flashlight_is_on = false;
    public bool canHover = false;
    public bool hitEnemy = false;
    private int rayAngle = 15;
    private int segements = 5;  
    public AudioClip collectSound;
    void Start()
    {
        _CrossHairImage.SetActive(true);
        _HandImage.SetActive(false);
        beamCollission = FindObjectOfType<AISight>();
    }
    public void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector2(.5f, .5f));

        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;

    //    int startAngle = int.Parse(-rayAngle * (0.5f);
       // int finishAngle = rayAngle * 2;

      //  int increment = rayAngle / segements;

     //   for (int i = startAngle; i < finishAngle; i += increment)
     //   {
     //       targetPos = (Quaternion.Euler(0f, i, 0f) * transform.forward).normalized * range;


        //    if (Physics.Raycast(startPos, targetPos, out hit))
        //    {
        //        Debug.Log("Hit: " + hit.collider.gameObject.name);
        //    }
        //    Debug.DrawRay(startPos, targetPos, Color.red);
        //}


        // if (Physics.Raycast(transform.position, hit, (ray.direction + transform.forward).normalized, range))


        // ^ this is for sweeping raycast for enemy detection this is combined with flashlight model to create detection
        // If sweeping raycast hits an enemy collider then it will set bool hitEnemy to true.
        // If hitEnemy == true then beamcollision = true; else beamCollision = false;
        // V This is  is for single raycast picking up object

        EnemyHit();

        if (Physics.Raycast(ray, out hit, range))
        {
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
       
        if (hitEnemy)
        {
          //  Debug.Log("I'm in this method");
            if (flashlight_is_on)
            {
                beamCollission.collision = true;
            }
            else
            {
                beamCollission.collision = false;
            }
        }
    }
        
}
