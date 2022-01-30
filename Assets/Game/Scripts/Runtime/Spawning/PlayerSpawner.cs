using System.Linq;
using Game.Runtime.Entities;
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

		#endregion

		#region Public Methods

		/// <summary>
		/// Spawns a player
		/// </summary>
		/// <param name="playerPrefab">The prefab of the player</param>
		/// <returns>The player's Entity component</returns>
		public Entity SpawnPlayer(GameObject playerPrefab)
		{
			SpawnPoint spawnPoint = spawnPoints.FirstOrDefault(point => point.IsPointFree());

			if (spawnPoint == null)
			{
				Debug.LogError("No free spawn points!");
				return null;
			}

			GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
			return player.GetComponent<Entity>();
		}

		/// <summary>
		/// Spawns a single player
		/// </summary>
		public void RespawnPlayer(Entity playerEntity)
		{
			SpawnPoint spawnPoint = spawnPoints.FirstOrDefault(point => point.IsPointFree());

			if (spawnPoint == null)
			{
				Debug.LogError("No free spawn points!");
				return;
			}

			playerEntity.transform.position = spawnPoint.transform.position;
			playerEntity.Revive();
		}

		#endregion
	}
}