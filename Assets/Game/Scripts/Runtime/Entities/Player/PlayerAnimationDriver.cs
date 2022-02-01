using Game.Runtime.UI;
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

		[SerializeField]
		private AimArrow aimArrow;

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
			mover.OnMovementChanged += PlayRunEffect;
			mover.OnDirectionChanged += LookTowardsMovementDirection;
		}

		private void OnDestroy()
		{
			mover.OnJump -= PlayJumpEffect;
			mover.OnGroundedChanged -= PlayLandEffect;
			mover.OnMovementChanged -= PlayRunEffect;
			mover.OnDirectionChanged -= LookTowardsMovementDirection;
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
			animator.SetTrigger("Jump");
			if (jumpEffect != null)
			{
				Instantiate(jumpEffect, transform.position + jumpEffectOffset, Quaternion.identity);
			}
		}

		private void PlayLandEffect(bool grounded)
		{
			if (!grounded) return;

			animator.SetTrigger("Land");
			if (landEffect != null)
			{
				Instantiate(landEffect, transform.position + landEffectOffset, Quaternion.identity);
			}
		}

		private void PlayRunEffect(bool isRunning) {
			animator.SetBool("IsRunning", isRunning);
		}

		private void LookTowardsMovementDirection(float direction) {
			if(direction > 0f) {
				animator.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
			} else if(direction < 0f) {
				animator.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
			}
		}

		#endregion
	}
}
