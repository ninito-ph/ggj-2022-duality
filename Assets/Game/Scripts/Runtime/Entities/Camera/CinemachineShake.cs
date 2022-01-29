using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake instance;
        //Use CinemachineShake.instance.ShakeCamera(5f, 1f); to shake the camera.

        public CinemachineVirtualCamera cinemachineVirtualCamera;
        public float shakeTimer;

        private void Awake()
        {
            instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            ShakeCamera(5f, .1f);
        }

        public void ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }

        private void Update()
        {
            if(shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if(shakeTimer <= 0f)
                {
                    //Time Over!
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
            
        }
    }
}
