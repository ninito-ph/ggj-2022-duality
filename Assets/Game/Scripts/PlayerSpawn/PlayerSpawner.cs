using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        [SerializeField]
        private bool player1, player2, player3, player4;

        [SerializeField]
        private GameObject player_1, player_2, player_3, player_4;

        [SerializeField]
        private GameObject spawnPoint_1, spawnPoint_2, spawnPoint_3, spawnPoint_4;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            if(player1)
            {
                player_1.transform.position = spawnPoint_1.transform.position;
            }

            if (player2)
            {
                player_2.transform.position = spawnPoint_2.transform.position;
            }

            if (player3)
            {
                player_3.transform.position = spawnPoint_3.transform.position;
            }

            if (player4)
            {
                player_4.transform.position = spawnPoint_4.transform.position;
            }
        }
    }
}
