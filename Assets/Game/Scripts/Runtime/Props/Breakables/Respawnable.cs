using System.Collections;
using Game.Runtime.Props.Breakables;
using UnityEngine;

namespace Runtime.Props.Breakables {
    public class Respawnable : BaseBreakable {
        [SerializeField]
        private float respawnDelay = 5f;

        private Renderer _renderer;
        private Collider2D[] _colliders;
        private Rigidbody2D _rigidbody;
        private Vector3 _initialPosition;
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
            _colliders = GetComponentsInChildren<Collider2D>();
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
            _initialPosition = transform.position;
            _respawnDelay = new WaitForSeconds(respawnDelay);
        }

        protected virtual IEnumerator RespawnObject() {
            yield return _respawnDelay;
            _renderer.enabled = true;

            foreach(Collider2D collider in _colliders) {
                collider.enabled = true;
            }

            if(_rigidbody != null) {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
            }

            transform.position = _initialPosition;
        }

        protected virtual void OnDestroy() {
            if(_respawnObjectCoroutine != null) {
                StopCoroutine(_respawnObjectCoroutine);
            }
        }

        private void DisableObjectTemporarily() {
            _renderer.enabled = false;
            foreach(Collider2D collider in _colliders) {
                collider.enabled = false;
            }

            if(_rigidbody != null) {
                _rigidbody.Sleep();
            }

            _respawnObjectCoroutine = StartCoroutine(RespawnObject());
        }
    }
}
