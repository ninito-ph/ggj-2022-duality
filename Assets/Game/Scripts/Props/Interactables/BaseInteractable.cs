using UnityEngine;

namespace Game.Props.Interactables {
    public class BaseInteractable : BaseProp {
        [SerializeField]
        protected GameObject instancedParticle;

        /// <summary>
        /// Interact with this particular item.
        /// </summary>
        public virtual void Interact() {
            Instantiate(instancedParticle, transform.position, Quaternion.identity);
        }

        protected virtual void OnTriggerEnter(Collider collider) {
            Interact();
        }
    }
}
