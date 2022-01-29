using UnityEngine;
using Game.Editor.Entities;

namespace Game.Props.Interactables {
    public class Hazard : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                entity.Kill();
            }
        }
    }
}
