using Game.Runtime.Entities;
using Game.Runtime.Input;
using Game.Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;

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
		
		[SerializeField]
		private AimArrow aimArrow;

		[SerializeField]
		private PlayerInput playerInputProvider;

		private FrameInput _frameInput;

		[Header("Input Actions")]
		[SerializeField]
		private string horizontalAxisAction;

		[SerializeField]
		private string verticalAxisAction;

		[SerializeField]
		private string jumpAction;

		[SerializeField]
		private string shootAction;

		[SerializeField]
		private string swapElementAction;

		[SerializeField]
		private string aimAction;

		#endregion

		#region Protected Methods

		protected override void CollectInput()
		{
			Vector2 test = playerInputProvider.currentActionMap.FindAction(aimAction).ReadValue<Vector2>();
			
			_frameInput = new FrameInput
			{
				AxisMovement = new Vector2(
					playerInputProvider.currentActionMap.FindAction(horizontalAxisAction).ReadValue<float>(),
					playerInputProvider.currentActionMap.FindAction(verticalAxisAction).ReadValue<float>()),
				JumpPress = playerInputProvider.currentActionMap.FindAction(jumpAction).WasPressedThisFrame(),
				JumpRelease = playerInputProvider.currentActionMap.FindAction(jumpAction).WasReleasedThisFrame(),

				Fire = playerInputProvider.currentActionMap.FindAction(shootAction).WasPressedThisFrame(),
				SwapElement = playerInputProvider.currentActionMap.FindAction(swapElementAction).WasPressedThisFrame(),

				AxisAim = playerInputProvider.currentActionMap.FindAction(aimAction).ReadValue<Vector2>(),
			};

		}

		protected override void DistributeInput()
		{
			if (_frameInput.Fire && shootingProvider != null)
			{
				shootingProvider.Shoot();
			}

			if (aimArrow != null)
			{
				aimArrow.AxisAim = _frameInput.AxisAim;
			}

			if (movementProvider != null)
			{
				movementProvider.Input = _frameInput;
			}
		}

		#endregion
	}
}