using UnityEngine;
using System.Collections;

public class RayCast_Pickup_Items : MonoBehaviour
{

    private float range = 500f;
    public PickUpMatches _Matchbox;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public BatteryPickUp _Battery;
    public Camera cam;
    // public Crosshair_Manager manager;
    public bool canHover = false;

    void Start()
    {
        _CrossHairImage.SetActive(true);
        _HandImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _Matchbox = FindObjectOfType<PickUpMatches>();
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (canHover == true)
            {
                //Debug.Log("canHover is true");

                if (hit.collider.tag == "Matchbox" || hit.collider.tag == "Battery")
                {
                    // Debug.Log("I hit: " + hit.collider.tag);
                    Debug.Log("I'm looking at matchbox");
                    _HandImage.SetActive(true);
                    _CrossHairImage.SetActive(false);

                    if (Input.GetButton("Fire1"))
                    {
                        if (hit.collider.tag == "Matchbox")
                        {
                            _Matchbox.AddMatch();
                            canHover = false;
                            _HandImage.SetActive(false);
                            _CrossHairImage.SetActive(true);

                        }
                        else if (hit.collider.tag == "Battery")
                        {
                            _Battery.AddBatteries();
                            canHover = false;
                            _HandImage.SetActive(false);
                            _CrossHairImage.SetActive(true);
                        }
                    }
                }

                else// if (hoverHit.collider.tag != "MatchBox" || hoverHit.collider.tag != "Battery")
                {
                    //Debug.Log("I'm looking at nothing");
                    _HandImage.SetActive(false);
                    _CrossHairImage.SetActive(true);
                }
                
            }
        }
    }
}