using Game.Editor.Entities;
using UnityEngine;

namespace Game.Props.Interactables {
    public class Key : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                // Give the player a key, and add to it an OnDeath event so that it respawns when it dies.
            }
        }
    }
}
