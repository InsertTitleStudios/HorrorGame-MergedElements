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
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, .5f));

        Debug.DrawRay(ray.origin, (ray.direction + transform.forward).normalized * range, Color.green);
        Debug.DrawRay(ray.origin, (ray.direction + transform.forward + transform.right).normalized * range, Color.green);
        Debug.DrawRay(ray.origin, (ray.direction + transform.forward - transform.right).normalized * range, Color.green);

        if (Physics.Raycast(ray.origin, ray.direction + transform.forward, out hit, range))
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
            if (hit.collider.tag == "Small Enemy" || hit.collider.tag == "Large Enemy")
            {
                hitEnemy = true;
                
            }
            else if (hit.collider.tag != "Small Enemy" || hit.collider.tag != "Large Enemy")
            {
                hitEnemy = false;
            }
        }
        if (Physics.Raycast(ray.origin, ray.direction + (transform.forward + transform.right).normalized, out hit, range))
        {
            if (hit.collider.tag == "Small Enemy" || hit.collider.tag == "Large Enemy")
            {
                hitEnemy = true;
            }
            else if (hit.collider.tag != "Small Enemy" || hit.collider.tag != "Large Enemy")
            {
                hitEnemy = false;
            }
        }
        if (Physics.Raycast(ray.origin, ray.direction + (transform.forward - transform.right).normalized, out hit, range))
        {
            if (hit.collider.tag == "Small Enemy" || hit.collider.tag == "Large Enemy")
            {
                hitEnemy = true;
            }
            else if (hit.collider.tag != "Small Enemy" || hit.collider.tag != "Large Enemy")
            {
                hitEnemy = false;
            }
        }
        EnemyHit();       
    }

    public void EnemyHit()
    {
        if (hitEnemy)
        {
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
