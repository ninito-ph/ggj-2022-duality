using UnityEngine;

namespace Game.Runtime.Props {
    public class Bridge : MonoBehaviour {
        [SerializeField]
        private Animator animator;

        public void DrawBridge() {
            animator.SetBool("IsActive", true);
        }

        public void CloseBridge() {
            animator.SetBool("IsActive", false);
        }
    }
}
