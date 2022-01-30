using Game.Runtime.Entities;
using Game.Runtime.Entities.Bullet;
using UnityEngine;

namespace Game.Runtime.Props.Interactables {
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

        private DualType _dualType = DualType.Light;

        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                PlayWarpFeedback();

                if(_dualType == DualType.Light) {
                    entity.transform.position = lightWarpPoint.transform.position;
                } else {
                    entity.transform.position = darkWarpPoint.transform.position;
                }
            } else {
                Bullet bullet;

                if(collider.TryGetComponent<Bullet>(out bullet)) {
                    PlayShootFeedback();

                    _dualType = _dualType == DualType.Light ? DualType.Dark : DualType.Light;
                }
            }
        }

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
    }
}
