using Game.Editor.Entities;
using UnityEngine;

namespace Game.Props.Interactables {
    public class Portal : BaseInteractable {
        private Element portalElement = Element.Light;

        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                // Teleport entity to the warp point of the portal's current element.
            }
            // If it's a projectile, change the portal's element.
        }
    }
}
