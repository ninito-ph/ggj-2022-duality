using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Mana : MonoBehaviour
    {
        public static Mana instance;
        public GameObject manaColectable;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            //
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "Key")
            {
                manaColectable.SetActive(false);
            }
        }
    }
}
