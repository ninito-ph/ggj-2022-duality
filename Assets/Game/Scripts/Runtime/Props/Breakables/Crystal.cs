using Game.Runtime.Entities.Bullet;
using UnityEngine;

namespace Game.Runtime.Props.Breakables {
    public class Crystal : BaseBreakable {
        [SerializeField]
        private int initialHitPoints = 10;

        [SerializeField]
        private GameObject manaCrystalPrefab;

        private int _hitPoints;

        protected override void Start() {
            base.Start();

            _hitPoints = initialHitPoints;
        }

        protected override void Break() {
            Instantiate(manaCrystalPrefab, transform.position, Quaternion.identity);

            base.Break();
        }

        protected override void OnCollisionEnter2D(Collision2D collision) {
            Bullet bullet;

            if(collision.collider.TryGetComponent<Bullet>(out bullet)) {
                _hitPoints -= 1;
                if(_hitPoints <= 0) {
                    Break();
                }
            }
        }
    }
}
