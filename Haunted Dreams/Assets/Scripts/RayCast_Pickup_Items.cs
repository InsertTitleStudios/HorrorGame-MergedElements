using UnityEngine;
using System.Collections;

public class RayCast_Pickup_Items : MonoBehaviour
{
    private float range = 500f;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public Camera cam;
    public AISight beamCollission;
    public bool flashlight_is_on = false;
    public bool canHover = false;
    public bool hitEnemy = false;
    public AudioClip collectSound;
    void Start()
    {
        _CrossHairImage.SetActive(true);
        _HandImage.SetActive(false);
        beamCollission = FindObjectOfType<AISight>();
    }
    void Update()
    {
        

        /*

            if (Physics.Raycast(ray, out  (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = AISight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (hit.collider.tag == "Small Enemy" || hit.collider.tag == "Large Enemy")
            {
                hitEnemy = true;
                if (flashlight_is_on)
                {
                    beamCollission.collision = true;
                }
                else
                {
                    beamCollission.collision = false;
                }
            }
            else if (hit.collider.tag != "Small Enemy" || hit.collider.tag != "Large Enemy")
            {
                hitEnemy = false;
            }
            */
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(ray, out hit, range))
        {

            if (canHover == true)
            {
                if (hit.collider.tag == "Matchbox" || hit.collider.tag == "Battery")
                {
                    _HandImage.SetActive(true);
                    _CrossHairImage.SetActive(false);

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
}

