using UnityEngine;

namespace Game.Runtime.Props.Breakables {
    public class BaseBreakable : BaseProp {
        [SerializeField]
        protected GameObject instancedParticle;

        /// <summary>
        /// Break this particular item.
        /// </summary>
        public virtual void Break() {
            if(audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }

            if(instancedParticle != null) {
                Instantiate(instancedParticle, transform.position, Quaternion.identity);
            }
            // Play the corresponding sound effect.
            Destroy(this.gameObject);
        }
    }
}
