using Game.Runtime.Entities;
using Game.Runtime.Input;
using UnityEngine;

namespace Game.Entities.Input.Player
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

		private FrameInput _frameInput;

		#endregion

		#region Protected Methods

		protected override void CollectInput()
		{
			_frameInput = new FrameInput()
			{
				AxisMovement = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"),
					UnityEngine.Input.GetAxisRaw("Vertical")),
				JumpPress = UnityEngine.Input.GetKeyDown(KeyCode.Space),
				JumpRelease = UnityEngine.Input.GetKeyUp(KeyCode.Space),
				Fire = UnityEngine.Input.GetKeyDown(KeyCode.Mouse0)
			};
		}

		protected override void DistributeInput()
		{
			if (_frameInput.Fire && shootingProvider != null)
			{
				shootingProvider.Shoot();
			}

			if (movementProvider != null)
			{
				movementProvider.Input = _frameInput;
			}
		}

		#endregion
	}
}