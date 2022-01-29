using Game.Editor.Entities;
using Game.Props.Interactables;
using UnityEngine;

namespace Props.Interactables {
    public class ManaCrystal : BaseInteractable {
        [SerializeField]
        private float manaRestored = 25f;

        protected override void OnTriggerEnter(UnityEngine.Collider collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                // Restore the entity's current mana.
            }
        }
    }
}
