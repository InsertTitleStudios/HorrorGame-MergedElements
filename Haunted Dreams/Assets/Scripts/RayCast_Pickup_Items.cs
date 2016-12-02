using UnityEngine;
using System.Collections;

public class RayCast_Pickup_Items : MonoBehaviour {

	private float range = 500f;
    public PickUpMatches _Matchbox;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public BatteryPickUp _Battery;
   // public Crosshair_Manager manager;
    public bool canHover = false;
   

    // Update is called once per frame
    void Update()
    {
        _Matchbox = FindObjectOfType<PickUpMatches>();
        RaycastHit hoverHit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(ray, out hoverHit, range))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (canHover == true)
            {
                Debug.Log("canHover is true");
                if (hoverHit.collider.tag == "MatchBox" || hoverHit.collider.tag == "Battery")
                {
                    Debug.Log("I'm looking at matchbox");
                    _HandImage.SetActive(true);
                    _CrossHairImage.SetActive(false);


                    if (Input.GetButton("Fire1"))
                    {
                        if (hoverHit.collider.tag == "Matchbox")
                            {
                                _Matchbox.AddMatch();
                            }
                        else if (hoverHit.collider.tag == "Battery")
                            {
                                _Battery.AddBatteries();
                            }
                        }
                    }

                else if (hoverHit.collider.tag != "MatchBox" || hoverHit.collider.tag != "Battery")
                {
                    Debug.Log("I'm looking at nothing");
                    _HandImage.SetActive(false);
                    _CrossHairImage.SetActive(true);
                }
            }
            else
            {
                canHover = false;
                Debug.Log("hover is false");
            }   
        }
    }
}