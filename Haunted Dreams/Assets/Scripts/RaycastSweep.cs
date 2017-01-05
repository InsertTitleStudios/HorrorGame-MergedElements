using UnityEngine;
using System.Collections;

public class RaycastSweep : MonoBehaviour
{

    public float distance = 10f;
    public float fov = 45;
    public Camera cam;
    public float angle;
    //Changes the spread of raycasts
    public int segments = 30;
    //Segments equals number of lines however incorrect number of segments showing for lines
    public bool enemyHit = false;
    private Vector3 startPos;
    private Vector3 targetPos;
    private int startAngle;
    private int finishAngle;
    private int increment;
    public int angleTemp;

    public bool changeDirection = false;

    void Start()
    {

        
        targetPos = Vector3.zero;

        //startAngle = (int)(-_angle * 0.5);
        //finishAngle = (int)(_angle * 0.5);

        //        increment = (int)(_angle / segments);
        StartCoroutine("RaySonar");
    }

    IEnumerator RaySonar()
    {
        while (true)
        {          
            for (int i = -segments; i < segments; i++)
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0, 0.5f));

                float segmentIndex = Mathf.Abs(i);
                //Debug.Log("i = " + segmentIndex);

                angle = Mathf.Lerp(-fov / 2, fov / 2, segmentIndex / segments);

                Debug.Log(angle);

                yield return null;

                targetPos = (Quaternion.Euler(0, angle, 0) * transform.forward).normalized * distance;

                if (Physics.Raycast(ray.origin, targetPos, out hit))
                {
                    if (hit.collider.tag == "Small Enemy")
                    {
                        Debug.Log("Enemy");
                        enemyHit = true;
                    }
                    else if (hit.collider.tag == "Wall")
                    {
                        enemyHit = false;
                    }
                }
                Debug.DrawRay(ray.origin, targetPos, Color.red);
            }
        }
    }
}












            
            
           
               
        
 