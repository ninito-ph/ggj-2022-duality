using System.Collections;
using Game.Runtime.Entities;
using Runtime.Props.Breakables;
using UnityEngine;

namespace Game.Runtime.Props.Breakables {
    public class BreakableTile : Respawnable {
        [SerializeField]
        private float breakDelay = 0.5f;

        private WaitForSeconds _breakDelay;
        private Coroutine _breakCoroutine;
        private bool willBreak;

        protected override void Start() {
            base.Start();
            _breakDelay = new WaitForSeconds(breakDelay);
        }

        protected override void OnCollisionEnter2D(Collision2D collision) {
            Entity entity;

            if(!willBreak && collision.collider.TryGetComponent<Entity>(out entity)) {
                willBreak = true;
                _breakCoroutine = StartCoroutine(BreakAfterDelay());
            }
        }

        protected override void OnDestroy() {
            if(_breakCoroutine != null) {
                StopCoroutine(_breakCoroutine);
            }
        }

        protected override IEnumerator RespawnObject() {
            willBreak = false;
            return base.RespawnObject();
        }

        protected override void Break() {
            base.Break();
        }

        private IEnumerator BreakAfterDelay() {
            yield return _breakDelay;
            Break();
        }
    }
}
