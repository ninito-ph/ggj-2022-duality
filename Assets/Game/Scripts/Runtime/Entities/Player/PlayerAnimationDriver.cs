using System;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
	/// <summary>
	/// A component that drives player animations
	/// </summary>
	public sealed class PlayerAnimationDriver : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private PlayerMover mover;

		[Header("Configurations")]
		[SerializeField]
		private GameObject jumpEffect;
		
		[SerializeField]
		private Vector3 jumpEffectOffset;

		[SerializeField]
		private GameObject landEffect;
		
		[SerializeField]
		private Vector3 landEffectOffset;

		private bool _lastGroundedState = true;

		#endregion

		#region Unity Callbacks

		private void Start()
		{
			mover.OnJump += PlayJumpEffect;
			mover.OnGroundedChanged += PlayLandEffect;
		}

		private void OnDestroy()
		{
			mover.OnJump -= PlayJumpEffect;
			mover.OnGroundedChanged -= PlayLandEffect;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(transform.position + jumpEffectOffset, 0.1f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(transform.position + landEffectOffset, 0.1f);
		}

		#endregion

		#region Private Methods

		private void PlayJumpEffect()
		{
			if (jumpEffect != null)
			{
				Instantiate(jumpEffect, transform.position + jumpEffectOffset, Quaternion.identity);
			}
		}

		private void PlayLandEffect(bool grounded)
		{
			if (!grounded) return;

			if (landEffect != null)
			{
				Instantiate(landEffect, transform.position + landEffectOffset, Quaternion.identity);
			}
		}

		#endregion
	}
}