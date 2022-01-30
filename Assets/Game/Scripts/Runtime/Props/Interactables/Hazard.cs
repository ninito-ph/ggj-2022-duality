using UnityEngine;
using Game.Runtime.Entities;

namespace Game.Runtime.Props.Interactables {
    public class Hazard : BaseInteractable {
        protected override void OnTriggerEnter2D(Collider2D collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                PlayInteractionFeedback();
                entity.Kill();
            }
        }
    }
}
