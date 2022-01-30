using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ItemSpawner : MonoBehaviour
    {
        public static ItemSpawner instance;

        #region Timer
        public float initialTime = 10f;
        public float timeLeft = 10f;
        public bool startTimer = false;
        #endregion

        #region Items
        private int itemCount;
        private int maxItemCount;
        [SerializeField]
        private GameObject[] spawnPoints;
        #endregion

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            timeLeft = initialTime;
        }

        private void Update()
        {
            #region ItemCountVerificate
            if (itemCount >= maxItemCount)
            {
                itemCount = maxItemCount;
            }
            if(itemCount <= 0)
            {
                itemCount = 0;
            }
            #endregion

            #region WhenTimerStarts
            if (startTimer)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    SpawnItem();
                    Debug.Log("Timer is over!");
                    timeLeft = 10;
                    startTimer = false;
                }
            }
            #endregion

            #region StartsTheTimer
            foreach (GameObject mana in spawnPoints)
            {
                if(!mana.activeSelf)
                {
                    startTimer = true;
                }
            }
            #endregion

            #region ColectItemTest
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                itemCount--;
                spawnPoints[Random.Range(0, spawnPoints.Length)].SetActive(false);
                timeLeft = initialTime;
            }
            #endregion
        }

        void SpawnItem()
        {
            spawnPoints[Random.Range(0, spawnPoints.Length)].SetActive(true);
            itemCount++;
        }
    }
}
