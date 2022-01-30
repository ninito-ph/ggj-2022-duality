using System.Collections;
using Game.Runtime.Props.Breakables;
using UnityEngine;

namespace Game.Runtime.Props.Breakables {
    public class Box : BaseBreakable {
        [SerializeField]
        private float respawnDelay = 5f;

        private Renderer _renderer;
        private Collider2D _collider;
        private WaitForSeconds _respawnDelay;
        private Coroutine _respawnBoxCoroutine;

        protected override void Break() {
            if(audioClip != null) {
                audioSource.PlayOneShot(audioClip);
            }

            if(instancedParticle != null) {
                Instantiate(instancedParticle, transform.position, Quaternion.identity);
            }

            DisableBoxTemporarily();
        }

        protected override void Start() {
            base.Start();

            _renderer = GetComponent<Renderer>();
            _collider = GetComponent<Collider2D>();
            _respawnDelay = new WaitForSeconds(respawnDelay);
        }

        private void OnDestroy() {
            if(_respawnBoxCoroutine != null) {
                StopCoroutine(_respawnBoxCoroutine);
            }
        }

        private void DisableBoxTemporarily() {
            _renderer.enabled = false;
            _collider.enabled = false;
            _respawnBoxCoroutine = StartCoroutine(RespawnBox());
        }

        private IEnumerator RespawnBox() {
            yield return _respawnDelay;
            _renderer.enabled = true;
            _collider.enabled = true;
        }
    }
}
