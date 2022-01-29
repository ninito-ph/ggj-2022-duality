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

        private void Start()
        {
            SpawnKey();
        }

        private void Update()
        {
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
            key.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }
    }
}
