using System.Linq;
using UnityEngine;

namespace Game.Runtime.Spawning
{
	/// <summary>
	/// A class which spawns players
	/// </summary>
	public sealed class PlayerSpawner : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private SpawnPoint[] spawnPoints;

		[SerializeField]
		private GameObject playerPrefab;

		#endregion

		#region Public Methods

		/// <summary>
		/// Spawns a single player
		/// </summary>
		public void SpawnPlayer()
		{
			SpawnPoint spawnPoint = spawnPoints.FirstOrDefault(point => point.IsPointFree());

			if (spawnPoint == null)
			{
				Debug.LogError("No free spawn points!");
				return;
			}
			
			Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
		}
		
		#endregion
	}
}