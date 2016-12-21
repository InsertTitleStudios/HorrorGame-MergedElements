using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class BeamCollision : MonoBehaviour {



        public AISight enemy;
        public

        void Start()
        {
            enemy = FindObjectOfType<AISight>();
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Wall" && coll.tag == "Small Enemy")
            {
                enemy.collision = false;
            }

            else if (coll.tag == "Wall" && coll.tag == "Large Enemy")
            {
                enemy.collision = false;
            }
            else
            {
                enemy.collision = true;
            }
        }

    }
}
