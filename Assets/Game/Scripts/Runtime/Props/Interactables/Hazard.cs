using UnityEngine;
using Game.Runtime.Entities;

namespace Game.Runtime.Props.Interactables {
    public class Hazard : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                PlayInteractionFeedback();
                entity.Kill();
            }
        }
    }
}
