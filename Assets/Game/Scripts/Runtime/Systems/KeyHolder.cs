using Game.Runtime.Entities;
using UnityEngine;

namespace Game.Runtime.Systems {
    public class KeyHolder : MonoBehaviour {
        private bool _hasKey;

        public bool HasKey() {
            return _hasKey;
        }

        public void GetKey() {
            _hasKey = true;
        }

        public void LoseKey(Entity entity) {
            _hasKey = false;
            entity.OnDeath -= LoseKey;
        }
    }
}
