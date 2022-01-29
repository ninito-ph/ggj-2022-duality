using UnityEngine;

namespace Game.Props.Collectables {
    public class BaseCollectable : BaseProp {
        // This class might be useless since all collectables are also interactables, technically.
        // Collecting an item is done in the same way as interacting with an item,
        // so the actions are effectively the same.

        /// <summary>
        /// Collect this particular item.
        /// </summary>
        public virtual void Collect() {
            Debug.Log($"Collected object {name}");
        }
    }
}
