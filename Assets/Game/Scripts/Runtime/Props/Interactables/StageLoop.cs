using Game.Runtime.Entities;
using Game.Runtime.Entities.Bullet;
using UnityEngine;

namespace Game.Runtime.Props.Interactables {
    public class StageLoop : BaseInteractable {
        [SerializeField]
        private StageLoop oppositeLoopPoint;

        [SerializeField]
        private float extraLoopOffset;

        protected override void OnTriggerEnter2D(Collider2D collider) {
            Entity entity;

            if(collider.TryGetComponent<Entity>(out entity)) {
                LoopObjectPosition(entity.transform);
            } else {
                Bullet bullet;

                if(collider.TryGetComponent<Bullet>(out bullet)) {
                    LoopObjectPosition(bullet.transform);
                }
            }
        }

        private void LoopObjectPosition(Transform objectTransform) {
            Vector3 finalPosition = oppositeLoopPoint.transform.position;
            finalPosition.x -= (transform.position.x - objectTransform.position.x + extraLoopOffset);
            finalPosition.y = objectTransform.position.y;
            finalPosition.z = objectTransform.position.z;


            objectTransform.position = finalPosition;
        }
    }
}
