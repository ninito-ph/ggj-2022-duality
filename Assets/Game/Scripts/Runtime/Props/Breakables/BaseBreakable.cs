using Game.Runtime.Entities.Bullet;
using UnityEngine;

namespace Game.Runtime.Props.Breakables {
    public class BaseBreakable : BaseProp {
        [SerializeField]
        protected GameObject instancedParticle;

        /// <summary>
        /// Break this particular item.
        /// </summary>
        protected virtual void Break() {
            if(audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }

            if(instancedParticle != null) {
                Instantiate(instancedParticle, transform.position, Quaternion.identity);
            }

            if(audioClip != null) {
                Destroy(this.gameObject, audioClip.length);
            } else {
                Destroy(this.gameObject);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            Bullet bullet;

            if(collision.collider.TryGetComponent<Bullet>(out bullet)) {
                Break();
            }
        }
    }
}
