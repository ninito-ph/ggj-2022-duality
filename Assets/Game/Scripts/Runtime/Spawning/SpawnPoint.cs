using UnityEngine;

namespace Game.Runtime.Spawning
{
	/// <summary>
	/// A class that controls a single spawn points
	/// </summary>
	[RequireComponent(typeof(CircleCollider2D))]
	public sealed class SpawnPoint : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private CircleCollider2D pointCollider;

		[Header("Configurations")]
		[SerializeField]
		private LayerMask obstructionMasks;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			TryGetComponent(out pointCollider);
		}

		private void OnDrawGizmos()
		{
			if (pointCollider == null)
			{
				TryGetComponent(out pointCollider);
			}

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, pointCollider.radius);
		}
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Spawns a prefab at the point if it is free
		/// </summary>
		/// <param name="prefab">The prefab to try to spawn</param>
		public bool TrySpawn(GameObject prefab)
		{
			if (!IsPointFree()) return false;
			Instantiate(prefab, transform.position, Quaternion.identity);
			return true;
		}
		
		/// <summary>
		/// Checks whether the spawn point is free or obstructed
		/// </summary>
		/// <returns>Whether the spawn point is free</returns>
		public bool IsPointFree()
		{
			return Physics2D.OverlapCircle(transform.position, pointCollider.radius, obstructionMasks) == null;
		}

		#endregion
	}
}