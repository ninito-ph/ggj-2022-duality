using Game.Editor.Entities;
using UnityEngine;

namespace Game.Entities.Player
{
    /// <summary>
    /// A class that handles player input
    /// </summary>
    public sealed class PlayerInputHandler : InputHandler
    {
        #region Fields

        [Header("Dependencies")]
        [SerializeField]
        private Mover movementProvider;

        [SerializeField]
        private Shooter shootingProvider;
        
        private Vector2 _movementInput;
        private bool _fire;

        #endregion

        #region Protected Methods

        protected override void CollectInput()
        {
            _fire = Input.GetKeyDown(KeyCode.Mouse0);
            _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        protected override void DistributeInput()
        {
            if (_fire)
            {
                shootingProvider.Shoot();
            }
            
            movementProvider.SetQueuedMovement(_movementInput);
        }

        #endregion
    }
}
