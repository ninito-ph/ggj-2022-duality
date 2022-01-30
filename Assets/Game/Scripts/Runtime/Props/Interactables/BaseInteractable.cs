using UnityEngine;

namespace Game.Runtime.Props.Interactables {
    public class BaseInteractable : BaseProp {
        [SerializeField]
        protected GameObject instancedParticle;

        protected void PlayInteractionFeedback() {
            if(audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }

            if(instancedParticle != null) {
                Instantiate(instancedParticle, transform.position, Quaternion.identity);
            }
        }

        protected virtual void OnTriggerEnter(Collider collider) {
            PlayInteractionFeedback();
        }
    }
}
