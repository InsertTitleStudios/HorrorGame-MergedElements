using UnityEngine;

public class RayCast_Pickup_Items : MonoBehaviour
{

    private float range = 500f;
    public GameObject _HandImage;
    public GameObject _CrossHairImage;
    public Camera cam;
    public bool canHover = false;

    void Start()
    {
        _CrossHairImage.SetActive(true);
        _HandImage.SetActive(false);
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
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
                            _CrossHairImage.SetActive(true);
                        }
                        else if (hit.collider.tag == "Battery")
                        {
                            hit.collider.gameObject.GetComponent<BatteryPickUp>().AddBatteries();
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