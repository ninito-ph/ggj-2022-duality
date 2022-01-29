using Game.Editor.Entities;
using UnityEngine;

namespace Game.Props.Interactables {
    public class Door : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                // If it has a key, open the door, give its team a point and proceed to the next stage if applicable.
            }
        }
    }
}
