using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Props.Interactables {
    public class ManaCrystal : BaseInteractable {
        [SerializeField]
        private float manaRestored = 25f;

        protected override void OnTriggerEnter2D(Collider2D collider) {
            ManaWallet manaWallet;

            if(collider.TryGetComponent<ManaWallet>(out manaWallet)) {
                PlayInteractionFeedback();
                manaWallet.RestoreMana(manaRestored);

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
