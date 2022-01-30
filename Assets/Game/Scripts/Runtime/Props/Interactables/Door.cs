using Game.Runtime.Entities;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Props.Interactables {
    public class Door : BaseInteractable {
        private Animator animator;

        protected override void Start() {
            base.Start();
            animator = GetComponent<Animator>();
        }

        protected override void OnTriggerEnter(Collider collider) {
            KeyHolder keyHolder;

            if(collider.TryGetComponent<KeyHolder>(out keyHolder)) {
                if(keyHolder.HasKey()) {
                    PlayInteractionFeedback();
                    animator.Play("Unlock");
                    keyHolder.LoseKey(keyHolder.GetComponent<Entity>());
                    // Give this entity a point and proceed to the next stage if applicable.
                }
            }
        }
    }
}
