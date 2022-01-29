using UnityEngine;

namespace Game.Props.Breakables {
    public class BaseBreakable : BaseProp {
        [SerializeField]
        protected GameObject instancedParticle;

        /// <summary>
        /// Break this particular item.
        /// </summary>
        public virtual void Break() {
            Instantiate(instancedParticle, transform.position, Quaternion.identity);
            // Play the corresponding sound effect.
            Destroy(this.gameObject);
        }
    }
}
