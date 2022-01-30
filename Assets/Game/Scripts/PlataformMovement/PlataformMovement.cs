using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlataformMovement : MonoBehaviour
    {
        public GameObject plataform;
        public Transform pointA, pointB;
        public float movementSpeed = 5f;
        private Vector3 m_target;

        // Start is called before the first frame update
        void Start()
        {
            m_target = pointB.position;
        }

        // Update is called once per frame
        void Update()
        {
            MovePlataform();
        }

        public void MovePlataform()
        {
            plataform.transform.position = Vector3.MoveTowards(plataform.transform.position, m_target, movementSpeed * Time.deltaTime);

            if (plataform.transform.position == pointA.position)//Checks if the platform is at point A 
            {
                m_target = pointB.position;
            }

            if (plataform.transform.position == pointB.position)//Checks if the platform is at point B 
            {
                m_target = pointA.position;
            }
        }
    }
}
