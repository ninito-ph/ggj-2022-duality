using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraMovement : MonoBehaviour
    {
        public static CameraMovement instance;
        public GameObject fase_1_cam, fase_2_cam, fase_3_cam;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            #region CameraMovementTest
            if (Input.GetKey(KeyCode.Alpha1))
            {
                MoveToFase1();
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                MoveToFase2();
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                MoveToFase3();
            }
            #endregion
        }

        public void MoveToFase1()
        {
            fase_1_cam.SetActive(true);
            fase_2_cam.SetActive(false);
            fase_3_cam.SetActive(false);
        }

        public void MoveToFase2()
        {
            fase_1_cam.SetActive(false);
            fase_2_cam.SetActive(true);
            fase_3_cam.SetActive(false);
        }

        public void MoveToFase3()
        {
            fase_1_cam.SetActive(false);
            fase_2_cam.SetActive(false);
            fase_3_cam.SetActive(true);
        }
    }
}
