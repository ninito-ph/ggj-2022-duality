using Game.Runtime.Entities;
using UnityEngine;

namespace Game.Props.Interactables {
    public class Portal : BaseInteractable {
        [SerializeField]
        private GameObject lightWarpPoint;
        [SerializeField]
        private GameObject darkWarpPoint;

        [SerializeField]
        private AudioClip warpAudioClip;
        [SerializeField]
        private AudioClip shootAudioClip;

        [SerializeField]
        private GameObject warpInstancedParticle;
        [SerializeField]
        private GameObject shootInstancedParticle;

        private Element _portalElement = Element.Light;

        private void PlayWarpFeedback() {
            if(warpAudioClip != null) {
                audioSource.PlayOneShot(warpAudioClip);
            }

            if(warpInstancedParticle != null) {
                Instantiate(warpInstancedParticle, transform.position, Quaternion.identity);
            }
        }

        private void PlayShootFeedback() {
            if(shootAudioClip != null) {
                audioSource.PlayOneShot(shootAudioClip);
            }

            if(shootInstancedParticle != null) {
                Instantiate(shootInstancedParticle, transform.position, Quaternion.identity);
            }
        }

        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                PlayWarpFeedback();

                if(_portalElement == Element.Light) {
                    entity.transform.position = lightWarpPoint.transform.position;
                } else {
                    entity.transform.position = darkWarpPoint.transform.position;
                }
            }

            // If it's a projectile, change the portal's element.
        }
    }
}
