using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class KeySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject key;

        [SerializeField]
        private Transform[] spawnPoints;

        #region Timer
        public float initialTime = 10f;
        public float timeLeft = 10f;
        public bool startTimer = false;
        #endregion

        private void Start()
        {
            SpawnKey();
            timeLeft = initialTime;
        }

        private void Update()
        {
            #region WhenTimerStarts
            if (startTimer)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    SpawnKey();
                    Debug.Log("Timer is over!");
                    timeLeft = 10;
                    startTimer = false;
                }
            }
            #endregion

            #region ColectKeyTest
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                key.SetActive(false);
                ItemSpawner.instance.timeLeft = ItemSpawner.instance.initialTime;
            }
            #endregion
        }

        void SpawnKey()
        {
            key.SetActive(true);
            key.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }
    }
}
