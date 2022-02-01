using System.Collections;
using Game.Runtime.Props.Breakables;
using UnityEngine;

namespace Runtime.Props.Breakables {
    public class Respawnable : BaseBreakable {
        [SerializeField]
        private float respawnDelay = 5f;

        private Renderer _renderer;
        private Collider2D _collider;
        private WaitForSeconds _respawnDelay;
        private Coroutine _respawnObjectCoroutine;

        protected override void Break() {
            if(audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }

            if(instancedParticle != null) {
                Instantiate(instancedParticle, transform.position, Quaternion.identity);
            }

            DisableObjectTemporarily();
        }

        protected override void Start() {
            base.Start();

            _renderer = GetComponentInChildren<Renderer>();
            _collider = GetComponentInChildren<Collider2D>();
            _respawnDelay = new WaitForSeconds(respawnDelay);
        }

        protected virtual IEnumerator RespawnObject() {
            yield return _respawnDelay;
            _renderer.enabled = true;
            _collider.enabled = true;
        }

        protected virtual void OnDestroy() {
            if(_respawnObjectCoroutine != null) {
                StopCoroutine(_respawnObjectCoroutine);
            }
        }

        private void DisableObjectTemporarily() {
            _renderer.enabled = false;
            _collider.enabled = false;
            _respawnObjectCoroutine = StartCoroutine(RespawnObject());
        }
    }
}
