using Game.Runtime.Entities;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Props.Interactables {
    public class Key : BaseInteractable {
        protected override void OnTriggerEnter(Collider collider) {
            KeyHolder keyHolder;

            if(collider.TryGetComponent<KeyHolder>(out keyHolder)) {
                PlayInteractionFeedback();
                keyHolder.GetKey();
                keyHolder.GetComponent<Entity>().OnDeath += keyHolder.LoseKey;

                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                if(audioClip != null) {
                    Destroy(gameObject, audioClip.length);
                } else {
                    Destroy(gameObject);
                }
            }
        }
    }
}
