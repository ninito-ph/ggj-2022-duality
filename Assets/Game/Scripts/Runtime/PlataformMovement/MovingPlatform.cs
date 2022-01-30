using UnityEngine;

namespace Game
{
	/// <summary>
	/// A class that controls a moving platform
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public sealed class MovingPlatform : MonoBehaviour
	{
		#region Fields

		[Header("Configurations")]
		[SerializeField]
		private Transform pointA;

		[SerializeField]
		private Transform pointB;

		[SerializeField]
		private float movementSpeed = 5f;

		private Rigidbody2D _rigidbody;
		private Vector3 _target;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			TryGetComponent(out _rigidbody);
			_target = pointA.position;
		}

		private void FixedUpdate()
		{
			MovePlatform();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(pointA.position, 0.5f);
			Gizmos.DrawLine(pointA.position, pointB.position);
			Gizmos.DrawSphere(pointB.position, 0.5f);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Moves the platform to its target
		/// </summary>
		private void MovePlatform()
		{
			_rigidbody.position = Vector3.MoveTowards(transform.position, _target, movementSpeed * Time.fixedDeltaTime);

			if (transform.position == pointA.position) //Checks if the platform is at point A 
			{
				_target = pointB.position;
			}

			if (transform.position == pointB.position) //Checks if the platform is at point B 
			{
				_target = pointA.position;
			}
		}

		#endregion
	}
}