using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Props.Interactables {
    public class ManaCrystal : BaseInteractable {
        [SerializeField]
        private float manaRestored = 25f;

        protected override void OnTriggerEnter(UnityEngine.Collider collider) {
            ManaWallet manaWallet;

            if(collider.TryGetComponent<ManaWallet>(out manaWallet)) {
                PlayInteractionFeedback();
                manaWallet.RestoreMana(manaRestored);
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                Destroy(gameObject, audioClip.length);
            }
        }
    }
}
