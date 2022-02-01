using Game.Runtime.Entities.Bullet
;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Runtime.Props.Interactables {
    public class Lever : BaseInteractable {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private UnityEvent onEnableEvent;
        [SerializeField]
        private UnityEvent onDisableEvent;

        private bool _isEnabled = false;

        protected override void OnTriggerEnter2D(Collider2D collider) {
           

            if(collider.TryGetComponent<Bullet> (out Bullet bullet)) {
                PlayInteractionFeedback();
                Interact();
            }
        }

        private void Interact() {
            if(_isEnabled) {
                animator.SetBool("ON", false);
                onDisableEvent.Invoke();
            } else {
                animator.SetBool("ON", true);
                onEnableEvent.Invoke();
            }

            _isEnabled = !_isEnabled;
        }
    }
}
