using Game.Runtime.Entities;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Props.Interactables {
    public class Key : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            KeyHolder keyHolder;

            if(collider.TryGetComponent<KeyHolder>(out keyHolder)) {
                PlayInteractionFeedback();
                keyHolder.GetKey();
                keyHolder.GetComponent<Entity>().OnDeath += keyHolder.LoseKey;
            }
        }
    }
}
