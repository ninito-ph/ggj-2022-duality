using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint instance;
        public GameObject manaColectable;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            //
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Key"))
            {
                manaColectable.SetActive(false);
            }
        }
    }
}
