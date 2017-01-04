using UnityEngine;
using System.Collections;

public class RaycastSweep : MonoBehaviour
{

    public float distance = 10f;
    public int _angle = 360;
    //Changes the spread of raycasts
    public int segments = 30;
    //Segments equals number of lines however incorrect number of segments showing for lines
    public bool enemyHit = false;
    private Vector3 startPos;
    private Vector3 targetPos;
    private int startAngle;
    private int finishAngle;
    private int increment;

    void Start()
    {

        startPos = transform.position;
        targetPos = Vector3.zero;

        startAngle = (int)(-_angle * 0.5);
        finishAngle = (int)(_angle * 0.5);

        increment = (int)(_angle / segments);
        StartCoroutine("RaySonar");
    }

    IEnumerator RaySonar()
    {
        while (true)
        {
            RaycastHit hit;
            for (int i = startAngle; i <= finishAngle; i += increment)
            {

                Debug.Log("i = " + i);
                yield return new WaitForSeconds(0.0005f);
                targetPos = (Quaternion.Euler(0, i, 0) * transform.forward).normalized * distance;

                if (Physics.Raycast(startPos, targetPos, out hit))
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

                if (i == finishAngle)
                {
                    increment = -(int)(_angle / segments);
                }
                Debug.DrawRay(startPos, targetPos, Color.red);
            }
            
        }
             
    }
}




            
            
           
               
        
 